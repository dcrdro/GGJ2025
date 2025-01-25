using UnityEngine;

public class Yoyo : MonoBehaviour
{
    public enum Axis
    {
        X,Y,Z,
    }

    [SerializeField] private Axis axis;
    [SerializeField] private float amount;
    [SerializeField] private float speed = 1f;

    private bool up;
    private float value;


    private float Ease(float x)
    {
        return x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
    }

    private void Update()
    {
        if (up)
        {
            value += Time.deltaTime * speed;

            if (value >= 1f)
            {
                up = false;
            }
        }
        else
        {
            value -= Time.deltaTime * speed;
            if (value < 0)
            {
                up = true;
            }
        }

        switch (axis)
        {
            case Axis.X:
                transform.localPosition = new Vector3(Ease(value) * amount, 0, 0);
                break;
            
            case Axis.Y:
                transform.localPosition = new Vector3(0, Ease(value) * amount, 0);
                break;
            
            case Axis.Z:
                transform.localPosition = new Vector3(0, 0, Ease(value) * amount);
                break;
        }
    }
}
