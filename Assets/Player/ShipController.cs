using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ShipController : MonoBehaviour
    {

        [Header("Movement")]
        public float xySpeed;
        public float forwardSpeed;
        public Transform rotationTarget;
        public float lookSpeed;
        public float lookDistance;
        [Range(0.0f, 90.0f)]
        public float horizontalLeanLimit;
        public float horizontalLeanLerpTime;

        [Header("Braking")]
        public float brakeSpeed;
        public float brakeDuration;
        public float brakeZoom;
        public float brakeZoomTweenDuration;

        [Header("Boost")]
        public float boostSpeed;
        public float boostDuration;
        public float boostZoom;
        public float boostZoomTweenDuration;

        [Header("Shield")]
        public ShieldBehaviour myShield;

        [Header("Weapons")]
        public WeaponBehaviour myPrimaryWeapon;
        public WeaponBehaviour mySecondaryWeapon;
        public Transform myAimTargetFront;
        public Transform myAimTargetBack;
        public float aimTargetDistance;
        public List<WeaponBehaviour> weaponList = new List<WeaponBehaviour>();

        [Header("Aim Assist")]
        public float aimAssistRadius;
        public float aimAssistMaxDistance;

        [Header("Camera")]
        public Transform myCameraHolder;
        public CinemachineDollyCart myDolly;

        [Header("Sounds")]
        public AudioSource engineNoise;
        public AudioSource boostNoise;
        public AudioSource boostPunchNoise;
        public float boostFadeDuration;
        public float boostNoiseVolume;

        [Header("Other")]
        [Tooltip("Damage taken when running into a wall")]
        public float wallDamage;



        //Controls: w: up, s: down, a: left, d: right, shift: speed up, ctrl: slow down

        private bool invertHorizontal, invertVertical;
        private HealthSystem myHealthSystem;
        private CinemachineImpulseSource myImpulseSource;



        private void OnEnable()
        {
            References.thePlayer = this;
        }

        private void Start()
        {
            DOTween.SetTweensCapacity(2000, 100);
            SetDollySpeed(forwardSpeed);    
            engineNoise.Play();
            boostNoise.volume = 0;
            invertVertical = References.theGameController.PrefsGetBool("invertVertical");
            invertHorizontal = References.theGameController.PrefsGetBool("invertHorizontal");


            myHealthSystem = GetComponent<HealthSystem>();
            myImpulseSource = GetComponent<CinemachineImpulseSource>();


        }

        private void Update()
        {

            float verticalInput = Input.GetAxis("Vertical") * (invertVertical ? -1 : 1);
            float horizontalInput = Input.GetAxis("Horizontal") * (invertHorizontal ? -1 : 1);
            float speedInput = Input.GetAxis("Speed");

            LocalMove(horizontalInput, verticalInput, xySpeed);
            SetLookRotation(horizontalInput, verticalInput, lookSpeed);
            HorizontalLean(References.thePlayer.transform, horizontalInput, horizontalLeanLimit, horizontalLeanLerpTime);

            References.theCanvas.PrintToDebugOverlay(verticalInput.ToString(), 1);
            References.theCanvas.PrintToDebugOverlay(horizontalInput.ToString(), 2);
            References.theCanvas.PrintToDebugOverlay(speedInput.ToString(), 3);

            
            //Charged Shot
            if (Input.GetButton("Fire2"))
            {
                mySecondaryWeapon.ChargeAndFire(myAimTargetFront.position, false);
                myShield.ResetShieldRechargeRate();
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                mySecondaryWeapon.ChargeAndFire(myAimTargetFront.position, true);
                myShield.ResetShieldRechargeRate();
            }

            //Normal Shot
            else if (Input.GetButton("Fire1"))
            {
                GameObject assistedTarget = AimAssist();
                if (assistedTarget != null)
                {
                    myPrimaryWeapon.ChargeAndFire(assistedTarget.transform.position, true);
                } else
                {
                    myPrimaryWeapon.ChargeAndFire(myAimTargetFront.position, true);
                }
                
                myShield.ResetShieldRechargeRate();
            }

            //Charge Shield
            else if (Input.GetButton("Jump"))
            {
                myShield.ChargeShield();
            }

            //Reset Sheild Charge
            if (!Input.GetButton("Jump"))
            {
                myShield.ReduceOvercharge();
                myShield.ResetShieldRechargeRate();
            }

            bool brakeValue = false;
            bool boostValue = false;

            if (speedInput < 0)
            {
                //Slow down
                brakeValue = true;
            }
            else if (speedInput > 0)
            {
                //Speed up
                boostValue = true;
            }

            Brake(brakeValue);
            Boost(boostValue);
            
            References.theCanvas.ShowTargettingReticule(References.theCanvas.playerFrontAimingReticule, myAimTargetFront);
            References.theCanvas.ShowTargettingReticule(References.theCanvas.playerBackAimingReticule, myAimTargetBack);
            References.theCanvas.ShowHealthFraction(myHealthSystem.currentHealth / myHealthSystem.maxHealth);

        }

        private void OnTriggerEnter(Collider other)
        {

            //When crashing into a wall
            GameObject theirGameObject = other.gameObject;
            if (theirGameObject.CompareTag(References.wallsTag))
            {
                bool damageDealt = myHealthSystem.TakeDamage(wallDamage);
                if (damageDealt)
                {
                    Debug.Log("Ship Crashed");
                    //Shake screen
                    myImpulseSource.GenerateImpulse();
                }

            }
        }

        public GameObject AimAssist()
        {
            if (Physics.SphereCast(transform.position, aimAssistRadius, transform.forward, out RaycastHit nearestTarget, aimAssistMaxDistance, References.enemiesLayer))
            {
                if (nearestTarget.transform.gameObject.CompareTag(References.enemiesTag))
                {
                    Debug.Log(nearestTarget.transform.gameObject.name);
                    return nearestTarget.transform.gameObject;
                }
            }

            return null;
        }


        private void SetLookRotation(float h, float v, float speed)
        {
            rotationTarget.parent.position = Vector3.zero;
            rotationTarget.localPosition = new Vector3(h, v, lookDistance);
            Quaternion targetRotation = Quaternion.LookRotation(rotationTarget.position);
            var step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
            References.theCanvas.PrintToDebugOverlay(step.ToString(), 4);
        }

        private void LocalMove(float x, float y, float speed)
        {
            transform.localPosition += speed * Time.deltaTime * new Vector3(x, y, 0);
            ClampPosition();
        }

        private void ClampPosition()
        {
            Vector3 pos = References.theCamera.WorldToViewportPoint(transform.position);
            pos.x = Mathf.Clamp01(pos.x);
            pos.y = Mathf.Clamp01(pos.y);
            transform.position = References.theCamera.ViewportToWorldPoint(pos);
        }

        private void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
        {
            Vector3 targetEulerAngels = target.localEulerAngles;
            

            target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
        }

        private void SetDollySpeed(float speed)
        {
            myDolly.m_Speed = speed;
            if (References.theEnemyDolly != null)
            {
                References.theEnemyDolly.setSpeed(speed);
            }
        }

        private void Boost(bool state)
        {
            if (state)
            {
                myCameraHolder.GetComponentInChildren<CinemachineImpulseSource>().GenerateImpulse();
                if (!boostNoise.isPlaying)
                {
                    boostPunchNoise.Play();
                    boostNoise.Play();
                    StartCoroutine(FadeAudioSource.StartFade(boostNoise, boostFadeDuration, boostNoiseVolume));
                }
                //trail.Play();
                //circle.Play();
            }
            else
            {
                if (boostNoise.isPlaying)
                {
                    StartCoroutine(FadeAudioSource.StartFade(boostNoise, boostFadeDuration, 0));
                    if (boostNoise.volume <= 0)
                    {
                        boostNoise.Stop();
                    }
                }
                //trail.Stop();
                //circle.Stop();
            }
            //trail.GetComponent<TrailRenderer>().emitting = state;

            //float origFov = state ? 40 : 55;
            //float endFov = state ? 55 : 40;
            //float origChrom = state ? 0 : 1;
            //float endChrom = state ? 1 : 0;
            //float origDistortion = state ? 0 : -30;
            //float endDistorton = state ? -30 : 0;
            //float starsVel = state ? -20 : -1;
            float speed = state ? forwardSpeed * 2 : forwardSpeed;
            float zoom = state ? boostZoom : 0;

            //DOVirtual.Float(origChrom, endChrom, .5f, Chromatic);
            //DOVirtual.Float(origFov, endFov, .5f, FieldOfView);
            //DOVirtual.Float(origDistortion, endDistorton, .5f, DistortionAmount);
            //var pvel = stars.velocityOverLifetime;
            //pvel.z = starsVel;

            DOVirtual.Float(myDolly.m_Speed, speed, .15f, SetDollySpeed);
            SetCameraZoom(zoom, boostZoomTweenDuration);


        }

        private void Brake(bool state)
        {
            //Question Mark and : explanation
            //(condition) ? [true path] : [false path];

            //speed = if (state == true) { forwardSpeed / 3 } else { forwardSpeed };
            float speed = state ? brakeSpeed : forwardSpeed;
            //zoom = if (state == true) { 3 } else { 0 }
            float zoom = state ? brakeZoom : 0;

            /*
            Tweening generates intermediate frames between two points
            This function takes a float value and tweens it
            In this case, we're tweening the speed of the dolly, starting at the current speed and tweening for 0.15 secs, ending at the speed value
            Once done, we call SetDollySpeed, passing it the speed value
            I don't know what the 0.15f actual changes - I feel no changes when testing different values */
            DOVirtual.Float(myDolly.m_Speed, speed, 0.15f, SetDollySpeed);

            SetCameraZoom(zoom, brakeZoomTweenDuration);
        }

        public GameObject GetClosestEnemyToAimTarget()
        {
            {
                GameObject[] gos;
                gos = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject closest = null;
                float distance = Mathf.Infinity;
                Vector3 position = myAimTargetFront.transform.position;
                foreach (GameObject go in gos)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = go;
                        distance = curDistance;
                    }
                }
                return closest;
            }
        }





        //Camera Effects
        void SetCameraZoom(float zoom, float duration)
        {
            myCameraHolder.DOLocalMove(new Vector3(0, 0, zoom), duration);
        }
        /*
        void DistortionAmount(float x)
        {
            References.theCamera.GetComponent<PostProcessVolume>().profile.GetSetting<LensDistortion>().intensity.value = x;
        }
        void FieldOfView(float fov)
        {
            myCameraHolder.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov;
        }
        void Chromatic(float x)
        {
            References.theCamera.GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>().intensity.value = x;
        }
        */

        private void OnDestroy()
        {
            References.thePlayer = null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(rotationTarget.position, .5f);
            Gizmos.DrawSphere(rotationTarget.position, .15f);
            

        }

    }
}
