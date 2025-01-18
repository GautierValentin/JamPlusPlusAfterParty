using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int attackPower;

    public ParticleSystem deathEffect;

    //Start is called once before the first frame
    virtual public void Start()
    {

    }

    // Update is called once per frame
    virtual public void Update()
    {
        
    }

    public void AddAttackPower(int amount)
    {
        attackPower += amount;
    }

    public void ChangeAnimation(string fileName, float crossfade = 0.2f)
    {
        GetComponent<Animator>().CrossFade(fileName, crossfade);
    }

    public void PlayDeathEffect()
    {
        deathEffect.Play();
    }

}
