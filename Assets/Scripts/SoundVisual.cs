using UnityEngine;

public class SoundVisual : MonoBehaviour
{
    private const int SAMPLE_SIZE = 1024;
    public float rmsV;
    public float dbV;
    public float pitchV;
    public float maxVisualScale = 25.0f;
    public float visualModifier = 50.0f;
    public float smoothSpeed = 10f;
    public float keepPersontage = 0.5f;
    public AudioSource music;
    private float[] samples = new float[SAMPLE_SIZE];
    private float[] spectrums = new float[SAMPLE_SIZE];
    private float sampleRate;

    private Transform[] visualList;
    private float[] visualScale;
    public int amnVisual = 64;

    private void Start()
    {
        sampleRate = AudioSettings.outputSampleRate;
        //SpawnLine();
        SpawnCircle();
    }

    private void SpawnLine()
    {
        visualList = new Transform[amnVisual];
        visualScale = new float[amnVisual];

        for (int i = 0; i < amnVisual; i++)
        {
            GameObject cub = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;
            visualList[i] = cub.transform;
            visualList[i].position = Vector3.right * i;
        }
    }
    
    private void SpawnCircle()
    {
        visualList = new Transform[amnVisual];
        visualScale = new float[amnVisual];

        Vector3 center = Vector3.zero;
        float radius = 6.0f;
        
        for (int i = 0; i < amnVisual; i++)
        {
            float ang = i * 1.0f / amnVisual;
            ang = ang * Mathf.PI * 2;
            float x = center.x + Mathf.Cos(ang) * radius;
            float y = center.y + Mathf.Sin(ang) * radius;
            
            Vector3 pos  = new Vector3(x, y ,0);
            GameObject cub = GameObject.CreatePrimitive(PrimitiveType.Cube) as GameObject;

            cub.transform.position = pos;
            cub.transform.rotation = Quaternion.LookRotation(Vector3.forward, pos);
            
            visualList[i] = cub.transform;
        }
    }

    private void Update()
    {
        music.GetOutputData(samples, 0);

        float sum = 0;
        for (int i = 0; i < SAMPLE_SIZE; i++)
        {
            sum = samples[i] * samples[i];
        }

        rmsV = Mathf.Sqrt(sum / SAMPLE_SIZE);
        dbV = 20 * Mathf.Log10(rmsV / 0.1f);
        music.GetSpectrumData(spectrums, 0, FFTWindow.Triangle);

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        int visualindex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)(SAMPLE_SIZE * keepPersontage) / amnVisual;

        while (visualindex < amnVisual)
        {
            int j = 0;
            float sum = 0;
            while (j < averageSize)
            {
                sum += spectrums[spectrumIndex];
                spectrumIndex++;
                j++;
            }

            float scaleY = sum / averageSize * visualModifier;
            visualScale[visualindex] -= Time.deltaTime * smoothSpeed;

            if (visualScale[visualindex] < scaleY)
            {
                visualScale[visualindex] = scaleY;
            }

            if (visualScale[visualindex] > maxVisualScale)
            {
                visualScale[visualindex] = maxVisualScale;
            }

            visualList[visualindex].localScale = new Vector3(0.3f, 0.3f, 0.3f) + Vector3.up * visualScale[visualindex];
            visualindex++;
        }
    }
}