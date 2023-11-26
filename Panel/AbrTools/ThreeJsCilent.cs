using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Panel16.AbrTools
{
    public class ThreeJsCilent:IThreeJsClient
    {
        private readonly IJSRuntime _js;

        public ThreeJsCilent(IJSRuntime js)
        {
            _js = js;
        }

        public async Task CreateNewScene()
        {
            await _js.InvokeVoidAsync("methods3d.CreateScene");
        }

        public async Task TongOpen()
        {
            await _js.InvokeVoidAsync("window.ThreeObject.unlatchSwivelToPipe");

        }
        public async Task TongClose()
        {
            await _js.InvokeVoidAsync("window.ThreeObject.latchSwivelToPipe");

        }
        
        public async Task InitializeHook(double height)
        {
            await _js.InvokeVoidAsync("window.ThreeObject.accelerateDownBase" , height );

        }
        
        public async Task AccelerateUp(double height)
        {
            await _js.InvokeVoidAsync("window.ThreeObject.accelerateUp" , height );
        }
        
        public async Task AccelerateDown(double height)
        {
            await _js.InvokeVoidAsync("window.ThreeObject.accelerateDown" , height );
        }
        
        public async Task ToggleSilipsStand()
        {
            await _js.InvokeVoidAsync("window.ThreeObject.toggleSilipsSitStand" );
        }
        
        public async Task ToggleTong()
        {
            await _js.InvokeVoidAsync("window.ThreeObject.toggleSwivelState" );
        }
        
        public async Task Drill()
        {
            await _js.InvokeVoidAsync("window.ThreeObject.dill" );
        }
        
        public async Task DrillRotary(double rpm)
        {
            await _js.InvokeVoidAsync("window.ThreeObject.drillRotary"  , rpm);
        }
    }
    public interface IThreeJsClient
    {
        Task CreateNewScene();
        Task TongOpen();
        Task TongClose();
        Task InitializeHook(double height);
        Task AccelerateUp(double height);
        Task AccelerateDown(double height);
        Task ToggleSilipsStand();
        Task ToggleTong();
        Task DrillRotary(double rpm);
        Task Drill();

    }
}
