/// <summary>
/// 씬들이 오픈 메서드를 반드시 재정의 하도록 부모 클래스 정의
/// </summary>
public abstract class Scene
{
    /// <summary>
    /// 각 씬에 들어갈 때 호출
    /// </summary>
    public abstract void Open();
}
