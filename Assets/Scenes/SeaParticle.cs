using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaParticle : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particlesArray;
    public Gradient colorGradient;
    public int seaResolution = 50; //ÿ�����ӵ���Ŀ
    public float spacing = 0.5f;//����֮��ļ��
    public float noiseScale = 0.2f; //
    public float heightScale = 3f;
    public float noiseX;
    public float noiseY;
    // Use this for initialization
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.loop = false;
        particlesArray = new ParticleSystem.Particle[seaResolution * seaResolution];
        particleSystem.maxParticles = seaResolution * seaResolution;
        particleSystem.Emit(seaResolution * seaResolution);
        particleSystem.GetParticles(particlesArray);
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {

                float zPos = Mathf.PerlinNoise(i * noiseScale, j * noiseScale) * heightScale;
                particlesArray[i * seaResolution + j].position = new Vector3(i * spacing, zPos, j * spacing);
            }
        }
        particleSystem.SetParticles(particlesArray, particlesArray.Length);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < seaResolution; i++)
        {
            for (int j = 0; j < seaResolution; j++)
            {

                float zPos = Mathf.PerlinNoise(i * noiseScale + noiseX, j * noiseScale + noiseY);
                //��������x��z�����ϲ���
                float r = Random.Range(5, 10);
                particlesArray[i * seaResolution + j].color = colorGradient.Evaluate(zPos);

                //��������x,y��ԭ����Ϊ�������Ӳ�������
                particlesArray[i * seaResolution + j].position = new Vector3((i + zPos * r) * spacing, zPos * heightScale, (j + zPos * r) * spacing);
            }
        }
        particleSystem.SetParticles(particlesArray, particlesArray.Length);
        noiseX += Time.deltaTime;
        noiseY += Time.deltaTime;
    }
}
