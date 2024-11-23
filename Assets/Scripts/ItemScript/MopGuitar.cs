using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopGuitar : BaseMusicItem
{
    [SerializeField] Transform Mop;
    [SerializeField] Transform FixPoint;
    // [SerializeField] Vector3 FixPointOffset;
    [SerializeField] private Transform rightHandCollider;
    [SerializeField] bool isSelected;
    [SerializeField] private GuitarMode guitarMode;
    [SerializeField] private float ModeARange;
    [SerializeField] private float ModeBRange;
    [SerializeField] private float ModeCRange;
    [SerializeField] private int soundIndex;
    [SerializeField] private int indexRange; //the number of sounds in list
    public Vector3 rotationOffset;
    float distanseOfLeftHand => getDistanceOfHand();


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("onTriggerEnter");
        AudioManager.Instance.PlaySound(ToString(guitarMode, soundIndex));
        soundIndex = (soundIndex + 1) % indexRange;
    }

    public override void BeSelected()
    {
        isSelected = true;
        Debug.Log("Mop be selected");
        StartCoroutine(UpdateRotation());
    }

    public override void UnSelected()
    {
        isSelected = false;
        Debug.Log("Mop be unselected");
        StopCoroutine(UpdateRotation());
    }

    private float getDistanceOfHand()
    {
        return Vector3.Distance(FixPoint.position, input.GetLeftHandPosition().position);
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckAttribute();
        soundIndex = 0;
        // Debug.Log(ToString(GuitarMode.A,7));//ModeA07

    }

    private void CheckAttribute()
    {
        if (!(ModeCRange > ModeARange && ModeBRange > ModeARange && ModeCRange > ModeBRange))
        {
            Debug.Log("Range Mode Attribute has problem");
        }
        if (input == null)
        {
            Debug.Log("Input is empty!");
        }

    }

    private static string ToString(GuitarMode mode, int index)
    {
        string result = "";
        switch (mode)
        {
            case GuitarMode.A:
                result += "ModeA";
                break;
            case GuitarMode.B:
                result += "ModeB";
                break;
            case GuitarMode.C:
                result += "ModeC";
                break;
        }
        result += index.ToString("D2");
        return result;
        //example for mode A with 7 index
        //ModeA07
    }

    private IEnumerator UpdateRotation()
    {
        while (isSelected)
        {
            Vector3 direction = input.GetLeftHandPosition().position - FixPoint.position;
            Mop.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(rotationOffset);
            Mop.position = FixPoint.position;
            rightHandCollider.position = input.GetRightHandPosition().position;
            yield return null;
        }
    }

    private void UpdateGuitarMode()
    {
        if (distanseOfLeftHand < ModeARange)
        {
            guitarMode = GuitarMode.A;
        }
        else if (distanseOfLeftHand < ModeBRange)
        {
            guitarMode = GuitarMode.B;
        }
        else if (distanseOfLeftHand < ModeCRange)
        {
            guitarMode = GuitarMode.C;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGuitarMode();
    }
}

public enum GuitarMode
{
    A,
    B,
    C
}
