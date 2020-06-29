using ApplicationService;
using System.Threading.Tasks;

namespace Application.OCR {
    public interface IOcr {
        Task<RecognitionResult> Recognize(File file);
    }
}
