using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour
{

    ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particlesArray;
    public int particleNumber = 700;
    public float radius = 2.0f;
    public float midR = 5.5f;
    public float maxR = 8.5f;
    public float free = 1.5f;//�����ζ��ķ�Χ  

    public float[] particleAngle;
    public float[] particleRadius;
    public float time = 0;
    GameObject gameObject_;
    bool move = true;
    public float speed = 0.15f;
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particlesArray = new ParticleSystem.Particle[particleNumber];//���������ӵ������ʼ��
        particleSystem.maxParticles = particleNumber;//�������ӷ�����������
        particleAngle = new float[particleNumber];
        particleRadius = new float[particleNumber];

        particleSystem.Emit(particleNumber);//���ոճ�ʼ����particleNumber�����ӷ����ȥ
        particleSystem.GetParticles(particlesArray);
        for (int i = 0; i < particleNumber; i++)
        {//Ϊÿ������������λ��
            //��С�뾶�������
            float rate1 = Random.Range(1.0f, midR / radius);
            //���뾶�����С
            float rate2 = Random.Range(midR / maxR, 1.0f);
            float r = Random.Range(radius * rate1, maxR * rate2);
            //float r = Random.Range(radius, 6.0f);
            float angle = Random.Range(0.0f, 360.0f);//λ��Ϊ0 - 360�ȵ����һ���Ƕ�
            float rad = angle / 180 * Mathf.PI;//�Ƕȱ任�ɻ���

            particleRadius[i] += Mathf.PingPong(time, free) - free / 2.0f;
            time += Time.deltaTime;

            particleAngle[i] = angle;
            particleRadius[i] = r;
            particlesArray[i].position = new Vector3(r * Mathf.Cos(rad), r * Mathf.Sin(rad), 0.0f);//Ϊÿ���������긳ֵ 
        }
        particleSystem.SetParticles(particlesArray, particlesArray.Length);//���ø�����ϵͳ�����ӡ�ǰ������ĳ������������ӵ�����
    }
    void Update()
    {
        //   Debug.Log(move);
        if (move)
        {
            for (int i = 0; i < particleNumber; i++)
            {
                //�����ٶ�Ϊ�����ͬ�ĵ���
                if (i % 2 == 0)
                {
                    particleAngle[i] += speed * (i % 80 + 1);
                }
                else
                {
                    particleAngle[i] -= speed * (i % 80 + 1);
                }
                if (particleAngle[i] > 360)
                    particleAngle[i] -= 360;
                if (particleAngle[i] < 0)
                    particleAngle[i] += 360;
                float rad = particleAngle[i] / 180 * Mathf.PI;
                particlesArray[i].position = new Vector3(particleRadius[i] * Mathf.Cos(rad), particleRadius[i] * Mathf.Sin(rad), 0f);
            }
            particleSystem.SetParticles(particlesArray, particleNumber);
        }
        //Debug.Log("111");
    }

    private void OnMouseDown()
    {


        if (move)
        {
            move = false;

        }
        else
        {
            move = true;

        }

    }
}

