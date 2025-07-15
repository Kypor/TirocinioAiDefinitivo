using UnityEngine;
using Unity.InferenceEngine;
using System.Collections;
using System.Collections.Generic;

public class IdeoRecognition : MonoBehaviour
{

    public Texture2D testPicture;
    public ModelAsset modelAsset;
    [SerializeField]
    UnityEngine.UI.Image screenshotCanva;
    DrawRandomIdeo drawRandomIdeo;
    public float[] results;
    private Worker worker;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        drawRandomIdeo = GameObject.Find("DrawRandomIdeoManager").GetComponent<DrawRandomIdeo>();
        Model model = ModelLoader.Load(modelAsset);

        FunctionalGraph graph = new FunctionalGraph();
        FunctionalTensor[] inputs = graph.AddInputs(model);
        FunctionalTensor[] outputs = Functional.Forward(model, inputs);

        FunctionalTensor softmax = Functional.Softmax(outputs[0]);

        Model runtimeModel = graph.Compile(softmax);
        worker = new Worker(runtimeModel, BackendType.GPUCompute);

        //RunAiIdeo2(testPicture);
        //RunAiDigit(testPicture);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine(TakeScreenshot());
        }
    }

    // public void RunAiIdeo(Texture2D picture)
    // {
    //     using Tensor<float> inputImage = TextureConverter.ToTensor(picture, 64, 64, 1);

    //     // Appiattisce l'immagine in un vettore 1x4096
    //     float[] flat = inputImage.DownloadToArray();
    //     TensorShape shape = new TensorShape(1, 4096);
    //     using Tensor<float> inputTensor = new Tensor<float>(shape, flat);

    //     worker.Schedule(inputTensor);

    //     Tensor<float> outputTensor = worker.PeekOutput() as Tensor<float>;
    //     results = outputTensor.DownloadToArray();

    //     int max = GetMaxIndex(results);
    //     Debug.Log(max);
    // }

    // public void RunAiIdeo2(Texture2D picture)
    // {
    //     // Debug.Log($"Originale: {picture.width}x{picture.height}");
    //     Texture2D resized = new Texture2D(128, 127);
    //     Graphics.ConvertTexture(picture, resized);
    //     // Debug.Log($"Resized: {resized.width}x{resized.height}");

    //     using Tensor<float> inputImage = TextureConverter.ToTensor(resized, 128, 127, 1);

    //     // Appiattisce l'immagine in un vettore 1x16256
    //     float[] flat = inputImage.DownloadToArray();
    //     TensorShape shape = new TensorShape(1, 16256);

    //     // // Applica la normalizzazione
    //     // float[] normalizedFlat = new float[flat.Length];
    //     // for (int i = 0; i < flat.Length; i++)
    //     // {
    //     //     normalizedFlat[i] = (flat[i] / 255.0f - 0.5f) / 0.5f;
    //     // }

    //     using Tensor<float> inputTensor = new Tensor<float>(shape, flat);
    //     worker.Schedule(inputTensor);
    //     Tensor<float> outputTensor = worker.PeekOutput() as Tensor<float>;
    //     results = outputTensor.DownloadToArray();

    //     int max = GetMaxIndex(results);
    //     Debug.Log(max);
    // }

    public void RunAiDigit(Texture2D picture)
    {
        using Tensor<float> inputTensor = TextureConverter.ToTensor(picture, 28, 28, 1);

        worker.Schedule(inputTensor);

        Tensor<float> outputTensor = worker.PeekOutput() as Tensor<float>;

        results = outputTensor.DownloadToArray();
        int max = GetMaxIndex(results);
        Debug.Log(max);
        Debug.Log(drawRandomIdeo.numbersIdeo[max]);
        drawRandomIdeo.ChangeTextCheck(max);

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
        testPicture = screenshot;
        // ScreenCapture.CaptureScreenshot(Application.dataPath + "/screenshot.png");
        RunAiDigit(testPicture);
        // solo per vedere il risutalto inutile sta parte 
        Sprite screenshotSprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f));
        screenshotCanva.enabled = true;
        screenshotCanva.sprite = screenshotSprite;
    }
}
