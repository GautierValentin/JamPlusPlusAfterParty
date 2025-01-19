using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int attackPower;

    public ParticleSystem deathEffect;

    public LinkDirection lookDirection = LinkDirection.TOP;

    public Transform powerCanvas;

    static protected Dictionary<LinkDirection, float> dictDirection = new Dictionary<LinkDirection, float>() {
        {LinkDirection.TOP, 0},
        {LinkDirection.TOPRIGHT, -33.69f},
        {LinkDirection.BOTRIGHT, 33.69f},
        {LinkDirection.BOT, 90.15f},
        {LinkDirection.BOTLEFT, 146.068f},
        {LinkDirection.TOPLEFT, -146.31f},
    };

    //Start is called once before the first frame
    virtual public void Start()
    {
        transform.rotation = Quaternion.Euler(0, dictDirection[lookDirection], 0);
        powerCanvas.localRotation = Quaternion.Euler(90, - dictDirection[lookDirection], 0);
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
