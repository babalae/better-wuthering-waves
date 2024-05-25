using BetterWutheringWaves.Core.Recognition;
using BetterWutheringWaves.GameTask.Model;
using OpenCvSharp;

namespace BetterWutheringWaves.GameTask.AutoSkip.Assets;

public class AutoSkipAssets : BaseAssets<AutoSkipAssets>
{
    public RecognitionObject StartAutoButtonRo;
    public RecognitionObject StopAutoButtonRo;

    public RecognitionObject SkipButtonRo;
    public RecognitionObject NotPromptAgainButtonRo;
    public RecognitionObject ConfirmButtonRo;

    private AutoSkipAssets()
    {
        StartAutoButtonRo = new RecognitionObject
        {
            Name = "StartAutoButton",
            RecognitionType = RecognitionTypes.TemplateMatch,
            TemplateImageMat = GameTaskManager.LoadAssetImage("AutoSkip", "start_auto.png"),
            RegionOfInterest = new Rect(CaptureRect.Width - CaptureRect.Width / 5, 0, CaptureRect.Width / 5, CaptureRect.Height / 8),
            DrawOnWindow = true
        }.InitTemplate();
        StopAutoButtonRo = new RecognitionObject
        {
            Name = "StopAutoButton",
            RecognitionType = RecognitionTypes.TemplateMatch,
            TemplateImageMat = GameTaskManager.LoadAssetImage("AutoSkip", "stop_auto.png"),
            RegionOfInterest = new Rect(CaptureRect.Width - CaptureRect.Width / 5, 0, CaptureRect.Width / 5, CaptureRect.Height / 8),
            DrawOnWindow = true
        }.InitTemplate();

        SkipButtonRo = new RecognitionObject
        {
            Name = "SkipButton",
            RecognitionType = RecognitionTypes.TemplateMatch,
            TemplateImageMat = GameTaskManager.LoadAssetImage("AutoSkip", "skip.png"),
            RegionOfInterest = new Rect(0, 0, CaptureRect.Width / 5, CaptureRect.Height / 8),
            DrawOnWindow = true
        }.InitTemplate();
        NotPromptAgainButtonRo = new RecognitionObject
        {
            Name = "NotPromptAgainButton",
            RecognitionType = RecognitionTypes.TemplateMatch,
            TemplateImageMat = GameTaskManager.LoadAssetImage("AutoSkip", "not_prompt_again.png"),
            RegionOfInterest = new Rect(CaptureRect.Width / 3, CaptureRect.Height / 2, CaptureRect.Width / 3, CaptureRect.Height / 2),
            DrawOnWindow = true
        }.InitTemplate();
        ConfirmButtonRo = new RecognitionObject
        {
            Name = "ConfirmButton",
            RecognitionType = RecognitionTypes.TemplateMatch,
            TemplateImageMat = GameTaskManager.LoadAssetImage("AutoSkip", "confirm.png"),
            RegionOfInterest = new Rect(CaptureRect.Width / 2, CaptureRect.Height / 2, CaptureRect.Width / 2, CaptureRect.Height / 2),
            DrawOnWindow = true
        }.InitTemplate();
    }
}
