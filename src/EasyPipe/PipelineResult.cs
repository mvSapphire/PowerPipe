namespace EasyPipe;

public abstract class PipelineResult
{
    public bool IsSucceed => string.IsNullOrEmpty(Error);

    public string Error { get; set; }
}