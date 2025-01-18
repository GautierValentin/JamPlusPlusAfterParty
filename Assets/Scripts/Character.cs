using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int attackPower;

    public ParticleSystem deathEffect;

    public LinkDirection lookDirection = LinkDirection.TOP;

    static private Dictionary<LinkDirection, float> dictDirection = new Dictionary<LinkDirection, float>() {
        {LinkDirection.TOP, 0},
        {LinkDirection.TOPRIGHT, 60},
        {LinkDirection.BOTRIGHT, 120},
        {LinkDirection.BOT, 180},
        {LinkDirection.BOTLEFT, 240},
         {LinkDirection.TOPLEFT, 240},
    };

    //Start is called once before the first frame
    virtual public void Start()
    {
        transform.rotation = Quaternion.Euler(0, dictDirection[lookDirection], 0);
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

    public Dictionary<LinkDirection, float> GetDirections()
    {
        return dictDirection;
    }

}
