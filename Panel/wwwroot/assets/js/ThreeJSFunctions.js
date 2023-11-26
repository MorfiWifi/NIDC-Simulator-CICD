import * as THREE from '../js/three.module.js';
import { ColladaLoader } from '../js/ColladaLoader.js';
import { OrbitControls } from '../js/OrbitControls.js';

function loadModel(id, model) {
    var container, clock, controls;
    var camera, scene, renderer, mixer, object;
    var rotation = {
        x: 0,
        y: 0,
        z: 0
    };

    function animate() {
        requestAnimationFrame(animate);

        if (object) {
            object.rotation.x += rotation.x;
            object.rotation.y += rotation.y;
            object.rotation.z += rotation.z;
        }
        render();
    }

    function render() {
        var delta = clock.getDelta();
        if (mixer !== undefined) {
            mixer.update(delta);
        }
        renderer.render(scene, camera);
    }

    function loadScene(id, model) {

        container = document.getElementById(id);
        if (!container) {
            return;
        }

        scene = new THREE.Scene();
        camera = new THREE.PerspectiveCamera(25, container.clientWidth / container.clientHeight, 1, 1000);

        clock = new THREE.Clock();

        var loader = new ColladaLoader();
        loader.load(model, function (collada) {
            scene.remove(scene.getObjectByName("object"));
            object = collada.scene;
            object.name = "object";
            scene.add(object);
            window.threes = {
                ...window.threes,
                [id]: {
                    ...window.threes[id],
                    object
                }
            };
        });



        var pointLight = new THREE.PointLight(0xffffff, 0.8);
        scene.add(camera);
        camera.add(pointLight);

        renderer = new THREE.WebGLRenderer({ antialias: true });
        renderer.setPixelRatio(window.devicePixelRatio);
        renderer.setSize(container.clientWidth, container.clientHeight);

        while (container.lastElementChild) {
            container.removeChild(container.lastElementChild);
        }

        container.appendChild(renderer.domElement);

        controls = new OrbitControls(camera, renderer.domElement);
        controls.screenSpacePanning = true;
        controls.minDistance = 5;
        controls.maxDistance = 40;
        controls.target.set(0, 2, 0);
        controls.update();

        animate();
        window.threes = {
            ...window.threes,
            [id]: {
                container, clock, controls,
                camera, scene, renderer, mixer, object,
                rotation
            }
        };
    }

    loadScene(id, model);
}


window.ThreeJSFunctions = {
    load: (id, model) => { loadModel(id, model) },

    act: (id, target, func, coef) => {
        if (!window.threes) return;
        var three = window.threes[id];
        if (!three) return;
        three[target][func](coef);
    },

    move: (id, target, orientation, x) => {
        window.ThreeJSFunctions.act(id, target, `translate${orientation.toUpperCase()}`, x);
    },
    rotate: (id, target, orientation, x) => {
        window.ThreeJSFunctions.act(id, target, `rotate${orientation.toUpperCase()}`, x);
    },
    rotating: (id, x, y, z) => {
        if (!window.threes) return;
        var three = window.threes[id];
        if (!three) return;
        three.rotation.x = x;
        three.rotation.y = y;
        three.rotation.z = z;
    }
};
