using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
        public float life = 3f;

        void Awake(){
            Destroy(gameObject, life);
        }

        void OnCollisionEnter(Collision collision){
            if(collision.gameObject.tag == "Dupe"){
                ScoreManager.instance.SubtractScore();
                ScoreManager.instance.LifeSubtract();
                //Destroy(collision.gameObject);
                }
            if(collision.gameObject.tag == "Bullseye"){
                ScoreManager.instance.AddScore();
                //Destroy(collision.gameObject);
                }
        Destroy(gameObject);
        }
}
