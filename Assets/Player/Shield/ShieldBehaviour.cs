using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ShieldBehaviour : MonoBehaviour
    {
        [Header("Limits")]
        public float maxRegularShield;
        public float overchargeValue;
        private float currentShieldValue;
        
        [Header("Recharging/Reducing")]
        public float baseRechargeSpeed;
        public float maxRechargeSpeed;
        public float rechargeIncreasePerSec;
        public float reduceOverchargeRate;
        private float currentRechargeSpeed;

        [Header("Overcharge Blast")]
        public GameObject myOverchargeShape;

        [Header("Sounds")]
        public AudioSource myChargingSound;
        public float chargeSoundStartPitch;
        public float chargeSoundEndPitch;


        // Start is called before the first frame update
        void Start()
        {
            currentShieldValue = 0;
        }

        // Update is called once per frame
        void Update()
        {
            References.theCanvas.ShowShieldFraction(currentShieldValue / maxRegularShield);
        }


        public void ChargeShield()
        {

            if (!myChargingSound.isPlaying)
            {
                myChargingSound.Play();
            }

            //Charge Shield
            currentShieldValue += currentRechargeSpeed * Time.deltaTime;
            //Increase shield recharge speed
            currentRechargeSpeed = Mathf.Min(currentRechargeSpeed + rechargeIncreasePerSec * Time.deltaTime, maxRechargeSpeed);

            //Get pitch based on currentvalue and percent of the charge that's been completed
            float percCompleted = currentShieldValue / overchargeValue;
            float currentPitch = percCompleted * (chargeSoundEndPitch - chargeSoundStartPitch);
            myChargingSound.pitch = currentPitch;
            
            
            //Overcharge
            if (currentShieldValue > overchargeValue)
            {
                OverchargeShield();
            }
        }

        public void ReduceOvercharge()
        {
            if (myChargingSound.isPlaying)
            {
                myChargingSound.Stop();
            }
            if (currentShieldValue > maxRegularShield)
            {
                currentShieldValue -= reduceOverchargeRate * Time.deltaTime;
            }
        }

        public void ResetShieldRechargeRate()
        {
            currentRechargeSpeed = baseRechargeSpeed;
        }

        private void OverchargeShield()
        {
            currentShieldValue = 0;
            Instantiate(myOverchargeShape, transform.position + transform.forward, transform.rotation);
        }
    }
}
