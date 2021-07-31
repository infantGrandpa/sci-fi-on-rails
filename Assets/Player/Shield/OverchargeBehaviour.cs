using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class OverchargeBehaviour : MonoBehaviour
    {
        [Header("Overcharge Blast")]
        public float overchargeDamage;
        public float startOverchargeScale;
        public float maxOverchargeScale;
        public float sizeIncreasePerSec;
        private float currentOverchargeScale;

        [Header("Light")]
        public Light myLight;
        public float lightIntensityIncreasePerSec;
        public float lightRangeMultiplier;

        [Header("Particle Effect")]
        public GameObject myParticlesObject;
        public GameObject myOpeningParticleObject;


        // Start is called before the first frame update
        void Start()
        {
            currentOverchargeScale = startOverchargeScale;
        }

        // Update is called once per frame
        void Update()
        {
            IncreaseBlastSize();
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject theirGameObject = other.gameObject;
            //Make sure we don't damage the player
            if (!GameObject.ReferenceEquals(theirGameObject, References.thePlayer.gameObject))
            {
                //Damage anythign with a health system
                HealthSystem theirHealthSystem = theirGameObject.GetComponentInParent<HealthSystem>();
                if (theirHealthSystem != null)
                {
                    theirHealthSystem.TakeDamage(overchargeDamage);
                }
            }
        }

        private void IncreaseBlastSize()
        {
            //Increase sphere scale
            currentOverchargeScale += sizeIncreasePerSec * Time.deltaTime;
            myLight.range = currentOverchargeScale * lightRangeMultiplier;

            //If greater than the max, end the overcharge attack
            if (currentOverchargeScale > maxOverchargeScale)
            {
                FadeOut();
            }
            else
            {
                Vector3 newScale = new Vector3(currentOverchargeScale, currentOverchargeScale, currentOverchargeScale);
                transform.localScale = newScale;

                myParticlesObject.transform.localScale = newScale;
                myOpeningParticleObject.transform.localScale = newScale;

                

            }
        }

        private void FadeOut()
        {
            myLight.intensity -= lightIntensityIncreasePerSec * Time.deltaTime;
            if (myLight.intensity == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}