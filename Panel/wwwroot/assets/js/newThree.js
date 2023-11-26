import * as THREE from '../js/three.module.js';
import Stats from '../js/stats.module.js';
import { ColladaLoader } from '../js/ColladaLoader.js';
import { OrbitControls } from '../js/OrbitControls.js';
import { HDRCubeTextureLoader } from '../js/HDRCubeTextureLoader.js';
import { RGBMLoader } from '../js/RGBMLoader.js';
import { GLTFLoader } from '../js/GLTFLoader.js';
import { DRACOLoader } from '../js/DRACOLoader.js';



let container, stats, elf, object, id ="profthreejs";
let camera, scene, renderer, controls;
let torusMesh, planeMesh, lightProbe, directionalLight;
let generatedCubeRenderTarget, ldrCubeRenderTarget, hdrCubeRenderTarget, rgbmCubeRenderTarget;
let ldrCubeMap, hdrCubeMap, rgbmCubeMap;
const API = {
	lightProbeIntensity: 1.0,
	directionalLightIntensity: 0.2,
	envMapIntensity: 1
};

//animate()
const params = {
	envMap: 'HDR',
	roughness: 0.0,
	metalness: 0.0,
	exposure: 1.0,
	debug: false
};


window.InitThree = initThreejs;

function initThreejs() {
	container = document.getElementById('profthreejs');
	console.log(container.clientHeight)
	console.log(container.clientWidth)


	camera = new THREE.PerspectiveCamera(40, container.clientWidth / container.clientHeight, 1, 1000);
	camera.position.set(0, 0, 120);
	

	scene = new THREE.Scene();
	scene.background = new THREE.Color(0x333333);
	lightProbe = new THREE.LightProbe();
	scene.add(lightProbe);

	// light
	directionalLight = new THREE.DirectionalLight(0xffffff, API.directionalLightIntensity);
	directionalLight.position.set(10, 10, 10);
	scene.add(directionalLight);
	renderer = new THREE.WebGLRenderer();
	renderer.useLegacyLights = false;
	renderer.toneMapping = THREE.ACESFilmicToneMapping;

	const loadingManager = new THREE.LoadingManager(function () {

		scene.add(elf);

	});

	// collada

	//const modelLoader = new ColladaLoader(loadingManager);
	//modelLoader.load('/assets/models/KB01.dae', function (collada) {
	//	scene.remove(scene.getObjectByName("object"));

	//	elf = collada.scene;

	//	elf.scale.x = 40
	//	elf.scale.y = 40
	//	elf.scale.z = 40

	//	elf.material = THREE.TextureLoader("../assets/models/Stormtrooper_D.jpg");

	//});
	const loader = new GLTFLoader();

	const dracoLoader = new DRACOLoader();
	dracoLoader.setDecoderPath('/assets/js/draco/');
	loader.setDRACOLoader(dracoLoader);


	loader.load('/assets/models/onlyDakal.gltf', function (gltf) {
		console.log(gltf)
		scene.add(gltf.scene);
		gltf.castShadow = true;
		gltf.receiveShadow = true;
		gltf.scene.scale.set(0.3, 0.3, 0.3)

		gltf.scene.children.forEach((obj) => {
			console.log(obj.name);
		})

		render();


	});

	const hemiLight = new THREE.HemisphereLight(0xffffff, 0xffffff, 0.6);
	hemiLight.color.setHSL(0.6, 1, 0.6);
	hemiLight.groundColor.setHSL(0.095, 1, 0.75);
	hemiLight.position.set(0, 50, 0);
	scene.add(hemiLight);
	const dirLight = new THREE.DirectionalLight(0xffffff, 500);
	dirLight.color.setHSL(0.1, 1, 0.95);
	dirLight.position.set(- 40, 60, 40);
	dirLight.position.multiplyScalar(300);
	scene.add(dirLight);

	const dirLight2 = new THREE.DirectionalLight(0xffff00, 100);
	dirLight2.color.setHSL(0.1, 1, 0.95);
	dirLight2.position.set(40, 60, 40);
	dirLight2.position.multiplyScalar(30);
	scene.add(dirLight2);

	dirLight.castShadow = true;

	dirLight.shadow.mapSize.width = 2048;
	dirLight.shadow.mapSize.height = 2048;

	const d = 50;

	dirLight.shadow.camera.left = - d;
	dirLight.shadow.camera.right = d;
	dirLight.shadow.camera.top = d;
	dirLight.shadow.camera.bottom = - d;

	dirLight.shadow.camera.far = 3500;
	dirLight.shadow.bias = - 0.0001;


	var pointLight = new THREE.PointLight(0xffffff, 0.8);
	scene.add(camera);
	camera.add(pointLight);



	THREE.DefaultLoadingManager.onLoad = function () {

		pmremGenerator.dispose();

	};

	const hdrUrls = ['px.hdr', 'nx.hdr', 'py.hdr', 'ny.hdr', 'pz.hdr', 'nz.hdr'];
	hdrCubeMap = new HDRCubeTextureLoader()
		.setPath('/assets/hdri/desert/hdr/')
		.load(hdrUrls, function () {

			hdrCubeRenderTarget = pmremGenerator.fromCubemap(hdrCubeMap);

			hdrCubeMap.magFilter = THREE.LinearFilter;
			hdrCubeMap.needsUpdate = true;

		});

	const ldrUrls = ['px.png', 'nx.png', 'py.png', 'ny.png', 'pz.png', 'nz.png'];
	ldrCubeMap = new THREE.CubeTextureLoader()
		.setPath('/assets/hdri/desert/')
		.load(ldrUrls, function () {

			ldrCubeMap.colorSpace = THREE.sRGBEncoding;

			ldrCubeRenderTarget = pmremGenerator.fromCubemap(ldrCubeMap);

		});


	const rgbmUrls = ['px.png', 'nx.png', 'py.png', 'ny.png', 'pz.png', 'nz.png'];
	rgbmCubeMap = new RGBMLoader().setMaxRange(16)
		.setPath('/assets/hdri/desert/')
		.loadCubemap(rgbmUrls, function () {

			rgbmCubeRenderTarget = pmremGenerator.fromCubemap(rgbmCubeMap);

		});

	const pmremGenerator = new THREE.PMREMGenerator(renderer);
	pmremGenerator.compileCubemapShader();



	renderer.setPixelRatio(window.devicePixelRatio);
	renderer.setSize(window.innerWidth, window.innerHeight);
	container.appendChild(renderer.domElement);

	renderer.outputColorSpace = THREE.sRGBEncoding;

	stats = new Stats();
	container.appendChild(stats.dom);
	controls = new OrbitControls(camera, renderer.domElement);
	controls.minDistance = 50;
	controls.maxDistance = 300;

	window.addEventListener('resize', onWindowResize);
	render();
	animate();
}

function onWindowResize() {

	const width = container.clientWidth;
	const height = container.clientHeight;

	camera.aspect = width / height;
	camera.updateProjectionMatrix();

	renderer.setSize(width, height);

}

function animate() {

	requestAnimationFrame(animate);

	stats.begin();
	render();
	stats.end();

}

function render() {


			let renderTarget, cubeMap;

			switch ( params.envMap ) {

				case 'Generated':
					renderTarget = generatedCubeRenderTarget;
					cubeMap = generatedCubeRenderTarget.texture;
					break;
				case 'LDR':
					renderTarget = ldrCubeRenderTarget;
					cubeMap = ldrCubeMap;
					break;
				case 'HDR':
					renderTarget = hdrCubeRenderTarget;
					cubeMap = hdrCubeMap;
					break;
				case 'RGBM16':
					renderTarget = rgbmCubeRenderTarget;
					cubeMap = rgbmCubeMap;
					break;

			}


		
			scene.background = cubeMap;
			renderer.toneMappingExposure = params.exposure;

			renderer.render( scene, camera );

		}