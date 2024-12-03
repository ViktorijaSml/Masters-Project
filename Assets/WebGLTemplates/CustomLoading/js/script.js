var messages = [     
"Your time is very important to us. Please wait while we ignore you.",   
"Still faster than Windows update.",      
"Kindly hold on until I finish a cup of coffee.",    
"We will be back in 1/0 minutes.",    
"Why don't you order a sandwich?",    
"Don't panic, Just count to infinite.",        
"Roses are red, violets are blue. Something went wrong: error 3502",    
"Press X to continue. Just kidding, we’re not ready."];


var message = document.getElementById("message");
message.textContent = messages[Math.floor(Math.random() * messages.length)]

var unityInstance = UnityLoader.instantiate("unityContainer", "Build/Astro Arena Web.loader.js", {onProgress: UnityProgress});
function UnityProgress (unityInstance, progress) {
  var progressPercent = Math.round(progress * 100);
  document.getElementById("loading-progress").innerHTML = progressPercent + "%";
}
      var container = document.querySelector("#unity-container");
      var canvas = document.querySelector("#unity-canvas");
      var loadingScreen = document.querySelector("#unity-custom-loading-screen");
      var customLoadingBar = document.querySelector("#custom-loader");
      var fullscreenButton = document.querySelector("#unity-fullscreen-button");
      var warningBanner = document.querySelector("#unity-warning");

      // Shows a temporary message banner/ribbon for a few seconds, or
      // a permanent error message on top of the canvas if type=='error'.
      // If type=='warning', a yellow highlight color is used.
      // Modify or remove this function to customize the visually presented
      // way that non-critical warnings and error messages are presented to the
      // user.
      function unityShowBanner(msg, type) {
        function updateBannerVisibility() {
          warningBanner.style.display = warningBanner.children.length ? 'block' : 'none';
        }
        var div = document.createElement('div');
        div.innerHTML = msg;
        warningBanner.appendChild(div);
        if (type == 'error') div.style = 'background: red; padding: 10px;';
        else {
          if (type == 'warning') div.style = 'background: yellow; padding: 10px;';
          setTimeout(function() {
            warningBanner.removeChild(div);
            updateBannerVisibility();
          }, 5000);
        }
        updateBannerVisibility();
      }

      var buildUrl = "Build";
      var loaderUrl = buildUrl + "/{{{ LOADER_FILENAME }}}";
      var config = {
        dataUrl: buildUrl + "/{{{ DATA_FILENAME }}}",
        frameworkUrl: buildUrl + "/{{{ FRAMEWORK_FILENAME }}}",
        codeUrl: buildUrl + "/{{{ CODE_FILENAME }}}",
        companyName: "{{{ COMPANY_NAME }}}",
        productName: "{{{ PRODUCT_NAME }}}",
        productVersion: "{{{ PRODUCT_VERSION }}}",
      };

      // By default Unity keeps WebGL canvas render target size matched with
      // the DOM size of the canvas element (scaled by window.devicePixelRatio)
      // Set this to false if you want to decouple this synchronization from
      // happening inside the engine, and you would instead like to size up
      // the canvas DOM size and WebGL render target sizes yourself.
      // config.matchWebGLToCanvasSize = false;

      if (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent)) {
        container.className = "unity-mobile";
        // Avoid draining fillrate performance on mobile devices,
        // and default/override low DPI mode on mobile browsers.
        config.devicePixelRatio = 1;
        unityShowBanner('');
      } else {
        //canvas.style.width = "960px";
        //canvas.style.height = "600px";
      }
	  
	  //canvas.width = window.innerWidth;
	  //canvas.height = window.innerHeight;
      loadingScreen.style.display = "block";

      var script = document.createElement("script");
      script.src = loaderUrl;
      script.onload = () => {
        createUnityInstance(canvas, config, (progress) => {
          unitySetLoaderProgressTo(progress);
        }).then((unityInstance) => {
          loadingScreen.style.display = "none";
        }).catch((message) => {
          alert(message);
        });
      };
      document.body.appendChild(script);

      // value - 0 to 1
      function unitySetLoaderProgressTo(value)
      {
        const fill = customLoadingBar.getElementsByClassName("fill")[0];
        const fillText = customLoadingBar.getElementsByClassName("label")[0];

        fill.animate(
          [
            { width: (value * 100) + "%" }
          ],
          {
            duration: 300,
            fill: "forwards"
          }
        );

        fillText.textContent = (value * 100).toFixed() + "%";
      }