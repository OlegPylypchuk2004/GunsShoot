using LeTai.Asset.TranslucentImage;
using UnityEngine;
using VContainer;

namespace UI
{
    public class BlurBackground : MonoBehaviour
    {
        [SerializeField] private TranslucentImage _translucentImage;

        [Inject]
        private void Construct(TranslucentImageSource translucentImageSource)
        {
            if (translucentImageSource == null)
            {
                _translucentImage.source = translucentImageSource;
            }
        }
    }
}