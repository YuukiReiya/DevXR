using Common;

namespace Game
{
    /// <summary>
    /// 素材のインターフェイス
    /// </summary>
    public interface IContent
    {
        Define.ContentsType Type { get; set; }
    }
}