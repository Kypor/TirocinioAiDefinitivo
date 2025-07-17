using UnityEngine;
using Unity.InferenceEngine;
using System.Collections;

public class IdeoRecognition : MonoBehaviour
{
    private Texture2D testPicture;
    public ModelAsset modelAsset;
    [SerializeField]
    UnityEngine.UI.Image screenshotCanva;
    DrawRandomIdeo drawRandomIdeo;
    public float[] results;
    private Worker worker;
    public int indexResult;
    void Start()
    {
        drawRandomIdeo = GameObject.Find("GameManager").GetComponent<DrawRandomIdeo>();
        Model model = ModelLoader.Load(modelAsset);

        FunctionalGraph graph = new FunctionalGraph();
        FunctionalTensor[] inputs = graph.AddInputs(model);
        FunctionalTensor[] outputs = Functional.Forward(model, inputs);

        FunctionalTensor softmax = Functional.Softmax(outputs[0]);

        Model runtimeModel = graph.Compile(softmax);
        worker = new Worker(runtimeModel, BackendType.GPUCompute);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(TakeScreenshot());
        }
    }
    public void RunAiIdeo(Texture2D picture)
    {
        Texture2D resized = new Texture2D(128, 128);
        Graphics.ConvertTexture(picture, resized);

        using Tensor<float> inputImage = TextureConverter.ToTensor(resized, 128, 128, 1);

        // Appiattisce l'immagine in un vettore 1x16256
        float[] flat = inputImage.DownloadToArray();
        TensorShape shape = new TensorShape(1, 16384);

        // Applica la normalizzazione
        float[] normalizedFlat = new float[flat.Length];
        for (int i = 0; i < flat.Length; i++)
        {
            normalizedFlat[i] = (flat[i] / 255.0f - 0.5f) / 0.5f;
        }

        using Tensor<float> inputTensor = new Tensor<float>(shape, flat);
        worker.Schedule(inputTensor);
        Tensor<float> outputTensor = worker.PeekOutput() as Tensor<float>;
        results = outputTensor.DownloadToArray();

        indexResult = GetMaxIndex(results);
        Debug.Log(indexResult);
        drawRandomIdeo.ToNextIdeosPartition(indexResult);
    }
    private void OnDisable()
    {
        worker.Dispose();
    }

    public int GetMaxIndex(float[] array)
    {
        int maxIndex = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > array[maxIndex])
            {
                maxIndex = i;
            }
        }

        return maxIndex;
    }
    private IEnumerator TakeScreenshot()
    {
        drawRandomIdeo.partitionIdeoImage.sprite = null;
        yield return new WaitForEndOfFrame();
        int width = 500;
        int height = 500;
        int startX = (Screen.width - width) / 2;
        int startY = (Screen.height - height) / 2;
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(startX, startY, width, height), 0, 0);
        screenshot.Apply();
        for (int y = 0; y < screenshot.height; y++)
        {
            for (int x = 0; x < screenshot.width; x++)
            {
                Color c = screenshot.GetPixel(x, y);
                float r = 1f - c.r;
                float g = 1f - c.g;
                float b = 1f - c.b;
                screenshot.SetPixel(x, y, new Color(r, g, b));
            }
        }
        screenshot.Apply();
        testPicture = screenshot;
        RunAiIdeo(testPicture);
        // solo per vedere il risutalto inutile sta parte 
        Sprite screenshotSprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f));
        screenshotCanva.enabled = true;
        screenshotCanva.sprite = screenshotSprite;
    }
}
