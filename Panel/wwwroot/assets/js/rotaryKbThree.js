import * as THREE from '../js/three.module.js';
import { OrbitControls } from '../js/OrbitControls.js';
import { GLTFLoader } from '../js/GLTFLoader.js';

let camera, scene, renderer, controls;
let container;

/**
 * Loaders
 */
const gltfLoader = new GLTFLoader()
const textureLoader = new THREE.TextureLoader()
const cubeTextureLoader = new THREE.CubeTextureLoader()
const sizes = {
    width: window.innerWidth,
    height: window.innerHeight
}
/**
 * Base
 */
const debugObject = {}

// Scene

window.Initi3d = initRotary3D;

/**
 * Animate
 */
const mousePosition = new THREE.Vector2()
const raycaster = new THREE.Raycaster()
let isMouseClicked = false

let isSilipsSitting = false;

const numberOfResources = 13
let numberOfLoadedResources = 0
let isSceneFullyLoaded = false
debugObject.envMapIntensity = 6
let colliderSilipsSit
let colliderSilipsStand
let pipeRathole = null
let pipeDrill = null
let rotary_table
let silips_mixer;
let silips_mesh;
let tong_mixer
let tong_mesh
let tong_collider_left
let tong_collider_right
let swivel_unlached = null
let swivel_lached = null
let kelly_bushing
let elevator = null
let drawworksCable = null
let deltaTime = 0

// const elevatorSpeed = 25
const maxSwivelDown = -113.33609999924883
const drillPipes = []
const distanceBetweenSwivelPosAndPipe = -93.62750000059606
const distanceBetweenElevatorPosAndPipe = -148.39249999746454

let drillSpeed = 0
let rotarySpeed = 0
let drillAcceleration = 3
let rotaryAcceleration = 3
let drawWorksMovementSpeed = .1
let drawWorksPos = 0

let isSceneInitialized = false
let keyDown = null
let canElevatorAccelerateDown = true
let isSwivelLatched = true
let isSwivelLatchedToPipes = false;
let isKellyBushingOnRotaryTable = false
let isElevatorOnLastPipe = false
let shouldTongsClose = true
let isDrilling = false


const cubeMaterial = new THREE.MeshBasicMaterial({
    envMap: new THREE.CubeTextureLoader().load([
        '/assets/texture/enviromentMap/px.jpg', '/assets/texture/enviromentMap/nx.jpg',
        '/assets/texture/enviromentMap/py.jpg', '/assets/texture/enviromentMap/ny.jpg',
        '/assets/texture/enviromentMap/pz.jpg', '/assets/texture/enviromentMap/nz.jpg'
    ]),
    side: THREE.BackSide // Ensure that the texture is on the inside of the cube
});


const AccelerateUp = (distanceUp) => {

    const distance = distanceUp;
    drillSpeed = 3;
    const MoveUpElevator = () => {
        elevator.position.y += distance
        MoveDrawworksCable(drawWorksPos - drawWorksMovementSpeed)

    }
    const MoveUpPipes = () => {
        drillPipes.forEach(pipe => {
            pipe.position.y += distance
        })
    }


    if (isSwivelLatchedToPipes) {
        if (isSilipsSitting) return
        MoveUpElevator()
        MoveUpPipes()
        isKellyBushingOnRotaryTable = false
    }
    else {
        MoveUpElevator()
        isKellyBushingOnRotaryTable = false
        isElevatorOnLastPipe = false

    }
    drillSpeed = 0;
    canElevatorAccelerateDown = true
}

const LatchSwivel = () => {
    swivel_unlached.traverse(child => {
        child.visible = false
    })
    swivel_lached.traverse(child => {
        child.visible = true
    })
    kelly_bushing.traverse(child => {
        child.visible = true
    })

    isSwivelLatched = true
}

const UnlatchSwivel = () => {
    swivel_lached.traverse(child => {
        child.visible = false
    })
    kelly_bushing.traverse(child => {
        child.visible = false
    })

    swivel_unlached.traverse(child => {
        child.visible = true
    })
    isSwivelLatched = false
}

const LatchSwivelToPipes = () => {
    // canElevatorAccelerateDown = true
    if (!isElevatorOnLastPipe) return

    isSwivelLatchedToPipes = true
    // canElevatorAccelerateDown = true
    // const lastPipe = drillPipes[drillPipes.length - 1]
    // elevator.add(lastPipe)
    // lastPipe.position.y = elevator.y
    // lastPipe.parent = elevator
    // elevator.add(lastPipe)
    if (tong_mesh) {
        debugObject.tong_close()
    }

    return isSwivelLatchedToPipes
}

const UnlatchSwivelFromPipes = () => {
    // canElevatorAccelerateDown = false
    isSwivelLatchedToPipes = false
    // const lastPipe = drillPipes[drillPipes.length - 1]
    // lastPipe.parent = null
    if (tong_mesh) {
        debugObject.tong_open()
    }

    return isSwivelLatchedToPipes
}
const AccelerateDown = (dis) => {
    if (!canElevatorAccelerateDown) return
    drillSpeed = -3
    const distance = -1 * dis;
    const LowerElevator = () => {
        elevator.position.y += distance
        MoveDrawworksCable(drawWorksPos + drawWorksMovementSpeed)

    }
    const LowerPipes = () => {
        drillPipes.forEach(pipe => {
            pipe.position.y += distance
        })
    }
    const PlaceSwivelOnRotaryTable = () => {
        isKellyBushingOnRotaryTable = true
        elevator.position.y = maxSwivelDown
    }
    const PlaceSwivelOnLastPipe = (pipePosition) => {
        elevator.position.y = pipePosition + distanceBetweenSwivelPosAndPipe
        isElevatorOnLastPipe = true

    }
    const PlaceElevatorOnLastPipe = (pipePosition) => {
        elevator.position.y = pipePosition + distanceBetweenElevatorPosAndPipe
        isElevatorOnLastPipe = true

    }
    const IsKellyBushingOnRotaryTable = () => {
        return elevator.position.y === maxSwivelDown
    }
    const CanKellyBushingSitOnRotaryTable = () => {
        return elevator.position.y + distance < maxSwivelDown
    }
    const IsKellyBushingOnLastPipe = (pipePosition) => {
        return elevator.position.y + pipePosition === distanceBetweenSwivelPosAndPipe
    }
    const CanKellyBushingSitOnLastPipe = (pipePosition) => {
        return elevator.position.y + distance - distanceBetweenSwivelPosAndPipe < pipePosition
    }
    const IsElevatorOnLastPipe = (pipePosition) => {
        return elevator.position.y + distanceBetweenElevatorPosAndPipe === pipePosition
    }
    const CanElevatorSitOnLastPipe = (pipePosition) => {
        return elevator.position.y + distance - distanceBetweenElevatorPosAndPipe < pipePosition
    }
    // (Rewriting)

    if (isSwivelLatched && isSwivelLatchedToPipes) {
        if (isSilipsSitting) return
        if (!IsKellyBushingOnRotaryTable()) {
            if (CanKellyBushingSitOnRotaryTable()) {
                PlaceSwivelOnRotaryTable()
                canElevatorAccelerateDown = false
            }
            else {
                LowerElevator()
                LowerPipes()
            }
        }
    }
    else if (isSwivelLatched && !isSwivelLatchedToPipes) {
        if (drillPipes.length > 0) {
            const lastPipe = drillPipes[drillPipes.length - 1]
            const lastPipePosition = lastPipe.position.y
            if (!IsKellyBushingOnLastPipe(lastPipePosition)) {
                if (CanKellyBushingSitOnLastPipe(lastPipePosition)) {
                    PlaceSwivelOnLastPipe(lastPipePosition)
                    canElevatorAccelerateDown = false
                }
                else {
                    LowerElevator()
                }
            }
        }
    }
    else if (!isSwivelLatched) {
        if (drillPipes.length > 0) {
            const lastPipe = drillPipes[drillPipes.length - 1]
            const lastPipePosition = lastPipe.position.y
            if (!IsElevatorOnLastPipe(lastPipePosition)) {
                if (CanElevatorSitOnLastPipe(lastPipePosition)) {
                    PlaceElevatorOnLastPipe(lastPipePosition)
                }
                else {
                    LowerElevator()
                }
            }
        }
    }
}
const AccelerateDownBase = (dis) => {
    if (!canElevatorAccelerateDown) return
    drillSpeed = -3
    const distance = dis - 148;
    const LowerElevator = () => {
        elevator.position.y += distance
        MoveDrawworksCable(drawWorksPos + drawWorksMovementSpeed)

    }
    const LowerPipes = () => {
        drillPipes.forEach(pipe => {
            pipe.position.y += distance
        })
    }
    const PlaceSwivelOnRotaryTable = () => {
        isKellyBushingOnRotaryTable = true
        elevator.position.y = maxSwivelDown
    }
    const PlaceSwivelOnLastPipe = (pipePosition) => {
        elevator.position.y = pipePosition + distanceBetweenSwivelPosAndPipe
        isElevatorOnLastPipe = true

    }
    const PlaceElevatorOnLastPipe = (pipePosition) => {
        elevator.position.y = pipePosition + distanceBetweenElevatorPosAndPipe
        isElevatorOnLastPipe = true

    }
    const IsKellyBushingOnRotaryTable = () => {
        return elevator.position.y === maxSwivelDown
    }
    const CanKellyBushingSitOnRotaryTable = () => {
        return elevator.position.y + distance < maxSwivelDown
    }
    const IsKellyBushingOnLastPipe = (pipePosition) => {
        return elevator.position.y + pipePosition === distanceBetweenSwivelPosAndPipe
    }
    const CanKellyBushingSitOnLastPipe = (pipePosition) => {
        return elevator.position.y + distance - distanceBetweenSwivelPosAndPipe < pipePosition
    }
    const IsElevatorOnLastPipe = (pipePosition) => {
        return elevator.position.y + distanceBetweenElevatorPosAndPipe === pipePosition
    }
    const CanElevatorSitOnLastPipe = (pipePosition) => {
        return elevator.position.y + distance - distanceBetweenElevatorPosAndPipe < pipePosition
    }
    // (Rewriting)

    if (isSwivelLatched && isSwivelLatchedToPipes) {
        if (isSilipsSitting) return
        if (!IsKellyBushingOnRotaryTable()) {
            if (CanKellyBushingSitOnRotaryTable()) {
                PlaceSwivelOnRotaryTable()
                canElevatorAccelerateDown = false
            }
            else {
                LowerElevator()
                LowerPipes()
            }
        }
    }
    else if (isSwivelLatched && !isSwivelLatchedToPipes) {
        if (drillPipes.length > 0) {
            const lastPipe = drillPipes[drillPipes.length - 1]
            const lastPipePosition = lastPipe.position.y
            if (!IsKellyBushingOnLastPipe(lastPipePosition)) {
                if (CanKellyBushingSitOnLastPipe(lastPipePosition)) {
                    PlaceSwivelOnLastPipe(lastPipePosition)
                    canElevatorAccelerateDown = false
                }
                else {
                    LowerElevator()
                }
            }
        }
    }
    else if (!isSwivelLatched) {
        if (drillPipes.length > 0) {
            const lastPipe = drillPipes[drillPipes.length - 1]
            const lastPipePosition = lastPipe.position.y
            if (!IsElevatorOnLastPipe(lastPipePosition)) {
                if (CanElevatorSitOnLastPipe(lastPipePosition)) {
                    PlaceElevatorOnLastPipe(lastPipePosition)
                }
                else {
                    LowerElevator()
                }
            }
        }
    }
    return "it is working";
}
const AddDrillPipe = () => {
    if (drillPipes.length > 0) {
        const lastpipe = drillPipes[drillPipes.length - 1]
        const newPipePosition = new THREE.Vector3()
        newPipePosition.x = lastpipe.position.x
        newPipePosition.y = lastpipe.position.y
        newPipePosition.z = lastpipe.position.z
        newPipePosition.add(new THREE.Vector3(0, 46.7, 0))
        const pipe = pipeDrill.clone()
        pipe.position.set(newPipePosition.x, newPipePosition.y, newPipePosition.z)
        drillPipes.push(pipe)
        scene.add(pipe)
    }
    else {
        const pipe = pipeDrill.clone()
        drillPipes.push(pipe)
        scene.add(pipe)
    }
}

const Drill = () => {
    if (!isDrilling) return

    if (!isKellyBushingOnRotaryTable) return
    const rotation = deltaTime * rotarySpeed
    rotary_table.rotation.y += rotation
    kelly_bushing.rotation.y += rotation
}
const MoveDrawworksCable = (z) => {
    if (z < -1 || z > 1) return
    const x = drawworksCable.position.x
    const y = drawworksCable.position.y
    const newZ = 3 * z + 1.8
    drawworksCable.position.set(x, y, newZ)
    drawWorksPos = z
}

const ProcessKeyDown = (key) => {
    switch (key) {
        case "w":
            AccelerateDown()
            break
        case "s":
            AccelerateUp()
            break
        case "e":
            Drill()
            break
        default:
            break
    }
}

const ProcessKeyPress = (key) => {
    switch (key) {
        case "l":
            if (isSwivelLatched) {
                UnlatchSwivel()
            }
            else {
                LatchSwivel()
            }
            isSwivelLatched = !isSwivelLatched
            break
        case "a":
            AddDrillPipe()
            break
        case "k":
            if (isSwivelLatchedToPipes) {
                UnlatchSwivelFromPipes()
            }
            else {
                LatchSwivelToPipes()
            }
            isSwivelLatchedToPipes = !isSwivelLatchedToPipes
            break
        default:
            break
    }
}

const ApplySilipsCollision = (raycaster) => {
    if (isKellyBushingOnRotaryTable) return

    if (silips_mesh && colliderSilipsSit && colliderSilipsStand) {
        if (isMouseClicked) {

            const silipsSitHitInfo = raycaster.intersectObject(colliderSilipsSit)
            const silipsStandHitInfo = raycaster.intersectObject(colliderSilipsStand)

            if (isSilipsSitting && silipsSitHitInfo.length > 0) {
                debugObject.silips_stand()
                isSilipsSitting = !isSilipsSitting
            }
            else if (!isSilipsSitting && silipsStandHitInfo.length > 0) {
                debugObject.silips_sit()
                isSilipsSitting = !isSilipsSitting
            }
        }
    }
}

const ApplyTongCollision = (raycaster) => {
    if (tong_mesh && tong_collider_left && tong_collider_right && isMouseClicked) {
        const tongLeftHitInfo = raycaster.intersectObject(tong_collider_left)
        const tongRightHitInfo = raycaster.intersectObject(tong_collider_right)
        if (tongLeftHitInfo.length > 0) {
            debugObject.tong_close()
            threeObject.latchSwivelToPipe()
        }
        else if (tongRightHitInfo.length > 0) {
            debugObject.tong_open()
            threeObject.unlatchSwivelToPipe()
        }
    }
}

const Initialize = () => {
    if (isSceneInitialized || !isSceneFullyLoaded) return
    AddDrillPipe()
    MoveDrawworksCable(0)

    isSilipsSitting = true
    debugObject.silips_sit()
    isSceneInitialized = true
}

const ProcessActions = () => {
    if (Math.abs(drillSpeed) > Math.E) {
        if (drillSpeed < 0) {
            AccelerateDown()
        }
        else {
            AccelerateUp()
        }
    }
    if (Math.abs(rotarySpeed) > Math.E) {
        Drill()
    }
}

const updateAllMaterials = () => {
    scene.traverse((child) => {
        if (child instanceof THREE.Mesh && child.material instanceof THREE.MeshStandardMaterial) {
            // child.material.envMap = environmentMap
            child.material.envMapIntensity = 6
            child.material.needsUpdate = true
            child.castShadow = true
            child.receiveShadow = true
        }
    })
}

const loadModelFinished = () => {
    numberOfLoadedResources++
    if (numberOfLoadedResources >= numberOfResources) {
        setTimeout(() => {
            const preloaderElm = document.querySelector("#rotaryKbThree .preloader")
            preloaderElm.style.display = "none"
            isSceneFullyLoaded = true
        }, 3000)
    }
}
const clock = new THREE.Clock()
let previousTime = 0
const tick = () => {
    camera.lookAt(0,50,0)
    const elapsedTime = clock.getElapsedTime()
    deltaTime = elapsedTime - previousTime
    previousTime = elapsedTime
    Initialize()
    if (silips_mixer) {
        silips_mixer.update(deltaTime)
    }
    if (tong_mixer) {
        tong_mixer.update(deltaTime)
    }

    ProcessActions()
    raycaster.setFromCamera(mousePosition, camera)
    ApplySilipsCollision(raycaster)
    ApplyTongCollision(raycaster)
    isMouseClicked = false
    renderer.render(scene, camera)
    window.requestAnimationFrame(tick)
}

function initRotary3D() {
    container = document.getElementById('threeContainer');
    scene = new THREE.Scene();
    camera = new THREE.PerspectiveCamera(75, container.clientWidth / container.clientHeight, 0.01, 1000)
    camera.position.set(20, 60, 20)
    camera.rotateY(Math.PI / 4)
    camera.enableRotate = true;
    camera.updateProjectionMatrix()
    const environmentMap = cubeTextureLoader.load([
        '/assets/hdri/desert/px.png',
        '/assets/hdri/desert/nx.png',
        '/assets/hdri/desert/py.png',
        '/assets/hdri/desert/ny.png',
        '/assets/hdri/desert/pz.png',
        '/assets/hdri/desert/nz.png'
    ])


    scene.environment = environmentMap;

    environmentMap.encoding = THREE.sRGBEncoding
    scene.environment = environmentMap
    gltfLoader.load(
        '/assets/models/Base.glb',
        (gltf) => {
            gltf.scene.position.set(0, .1, 0)
            scene.add(gltf.scene)
            updateAllMaterials()
            loadModelFinished()
        }
    )
    gltfLoader.load(
        '/assets/models/Colliders.glb',
        (gltf) => {
            colliderSilipsSit = gltf.scene.children[0]
            colliderSilipsStand = gltf.scene.children[1]
            const material = new THREE.MeshStandardMaterial();
            material.transparent = true
            material.opacity = 0
            gltf.scene.children.forEach(child => {
                child.material = material
            })
            scene.add(gltf.scene)
            updateAllMaterials()
            loadModelFinished()
        }
    )
    gltfLoader.load(
        '/assets/models/Tong_Placeholders.glb',
        (gltf) => {
            const material = new THREE.MeshStandardMaterial();
            material.transparent = true
            material.opacity = 0
            tong_collider_right = gltf.scene.children[0]
            tong_collider_left = gltf.scene.children[1]
            gltf.scene.children.forEach(child => {
                child.material = material
            })
            scene.add(gltf.scene)
            updateAllMaterials()
            loadModelFinished()
        }
    )
    gltfLoader.load(
        '/assets/models/Pipe_Rathole.glb',
        (gltf) => {
            pipeRathole = gltf.scene
            gltf.scene.position.set(0, .1, 0)
            scene.add(gltf.scene)
            updateAllMaterials()
            loadModelFinished()
        }
    )

    gltfLoader.load(
        '/assets/models/Pipe_Drill.glb',
        (gltf) => {
            pipeDrill = gltf.scene
            gltf.scene.position.set(0, .1, 0)
            updateAllMaterials()
            loadModelFinished()
        }
    )
    gltfLoader.load(
        '/assets/models/Drawworks_Cable.glb',
        (gltf) => {
            drawworksCable = gltf.scene
            gltf.scene.position.set(0, .1, 0)
            scene.add(gltf.scene)
            updateAllMaterials()
            loadModelFinished()
        }
    )

    gltfLoader.load(
        '/assets/models/Rotary_Table.glb',
        (gltf) => {
            rotary_table = gltf.scene
            gltf.scene.position.set(0, .1, 0)
            scene.add(gltf.scene)
            updateAllMaterials()
            loadModelFinished()
        }
    )
    gltfLoader.load(
        '/assets/models/Silips.glb',
        (gltf) => {
            silips_mesh = gltf.scene
            silips_mixer = new THREE.AnimationMixer(gltf.scene)
            const sit = silips_mixer.clipAction(gltf.animations[0])
            sit.setLoop(THREE.LoopOnce)
            sit.clampWhenFinished = true
            const stand = silips_mixer.clipAction(gltf.animations[1])
            stand.setLoop(THREE.LoopOnce)
            stand.clampWhenFinished = true
            debugObject.silips_sit = () => {
                silips_mixer.stopAllAction()
                sit.play()
            }
            debugObject.silips_stand = () => {
                silips_mixer.stopAllAction()
                stand.play()
            }
            scene.add(gltf.scene)
            updateAllMaterials()
            loadModelFinished()
        }
    )
    gltfLoader.load(
        '/assets/models/Tongs.glb',
        (gltf) => {
            tong_mesh = gltf.scene
            tong_mixer = new THREE.AnimationMixer(gltf.scene)
            const close = tong_mixer.clipAction(gltf.animations[1])
            close.setLoop(THREE.LoopOnce)
            close.clampWhenFinished = true
            const open = tong_mixer.clipAction(gltf.animations[2])
            open.setLoop(THREE.LoopOnce)
            open.clampWhenFinished = true
            debugObject.tong_open = () => {
                tong_mixer.stopAllAction()
                open.play()
            }
            debugObject.tong_close = () => {
                tong_mixer.stopAllAction()
                close.play()
            }
            scene.add(gltf.scene)
            updateAllMaterials()
            loadModelFinished()
        }
    )
    gltfLoader.load("/assets/models/Swivel_Unlatched.glb", (gltf) => {
        swivel_unlached = gltf.scene
        scene.add(swivel_unlached)
        swivel_unlached.traverse(child => {
            child.visible = false;
        })
        updateAllMaterials()
        loadModelFinished()
    })
    gltfLoader.load("/assets/models/Swivel_Latched.glb", (gltf) => {
        swivel_lached = gltf.scene
        scene.add(swivel_lached)
        updateAllMaterials()
        loadModelFinished()
    })
    gltfLoader.load(
        '/assets/models/Kelly_Bushing.glb',
        (gltf) => {
            kelly_bushing = gltf.scene
            gltf.scene.position.set(0, .1, 0)
            scene.add(gltf.scene)
            updateAllMaterials()
            loadModelFinished()
        }
    )
    gltfLoader.load("/assets/models/Elevator.glb", (gltf) => {
        elevator = gltf.scene
        swivel_lached.parent = elevator
        kelly_bushing.parent = elevator
        scene.add(elevator)
        updateAllMaterials()
        loadModelFinished()
    })
    const hemiLight = new THREE.HemisphereLight(0xffffff, 0xffffff,);
    hemiLight.color.setHSL(0.6, 1, 0.6);
    hemiLight.groundColor.setHSL(0.095, 1, 0.75);
    hemiLight.position.set(0, 50, 0);
    scene.add(hemiLight);
    const dirLight = new THREE.DirectionalLight(0xffffff, 1);
    dirLight.color.setHSL(0.1, 1, 0.95);
    dirLight.position.set(- 1, 1.75, 1);
    dirLight.position.multiplyScalar(30);
    scene.add(dirLight);
    const dirLight2 = new THREE.DirectionalLight(0xffff00, 1);
    dirLight2.color.setHSL(0.1, 1, 0.95);
    dirLight2.position.set(1, 1.75, 1);
    dirLight2.position.multiplyScalar(30);
    scene.add(dirLight2);
    dirLight.castShadow = true;
    dirLight.shadow.mapSize.width = 2048;
    dirLight.shadow.mapSize.height = 2048;
    const d = 50;
    renderer = new THREE.WebGLRenderer({
        antialias: true,
        alpha: true,
    })
    renderer.physicallyCorrectLights = true
    renderer.outputEncoding = THREE.sRGBEncoding
    // renderer.toneMapping = THREE.CineonToneMapping
    renderer.toneMapping = THREE.ACESFilmicToneMapping;
    renderer.toneMappingExposure = 1.75
    renderer.shadowMap.enabled = true
    renderer.shadowMap.type = THREE.PCFSoftShadowMap
    renderer.setClearColor( 0x000000, 0 )
    renderer.setSize(container.clientWidth, container.clientHeight);
    renderer.setPixelRatio(Math.min(window.devicePixelRatio, 2))
    container.appendChild(renderer.domElement);
    controls = new OrbitControls(camera, renderer.domElement);
    // controls.enableZoom = false; // Enable zoom
    // controls.enableRotate = true; //
    controls.minDistance = 0;
    controls.maxDistance = 300;
    controls.target.set(0, 4, 0);

// Listen for changes in controls
controls.addEventListener('change', function () {
    console.log('changing controls')
    // Update the target based on the controls' target
    // This assumes you want to update the target to the same position as the controls' target
    controls.target.clone();
    // You can use controls.target.x, controls.target.y, controls.target.z individually if needed
    // Use the updated target position for other purposes if necessary
    // For example, update some UI elements based on the new target position
});
    // controls.minZoom = 0.5; // Minimum zoom level
    // controls.maxZoom = 2.0;
    // controls.minPolarAngle = -(3 * Math.PI) / 4; // Minimum vertical angle (in radians)
    // controls.maxPolarAngle = (3 * Math.PI); 
    // controls.update();
    document.querySelector('canvas').addEventListener("mousemove", (e) => {
        const mouseX = e.clientX;
        const mouseY = e.clientY;
        const containerRect = container.getBoundingClientRect();
        const offsetX = containerRect.left;  // X offset of container
        const offsetY = containerRect.top;   // Y offset of container
        const mouseXRelativeToContainer = mouseX - offsetX;
        const mouseYRelativeToContainer = mouseY - offsetY;
        mousePosition.x = mouseXRelativeToContainer / container.clientWidth * 2 - 1
        mousePosition.y = - (mouseYRelativeToContainer / container.clientHeight * 2 - 1)
    })
    document.addEventListener("click", e => {
        isMouseClicked = true
    })
    document.addEventListener("keypress", e => {
        if (!isSceneFullyLoaded) return
        ProcessKeyPress(e.key.toLowerCase())
    })
    document.addEventListener("keydown", (e) => {
        keyDown = e.key.toLowerCase()
    })
    document.addEventListener("keyup", (e) => {
        keyDown = null
    })
    tick();
    window.addEventListener('resize', onWindowResize);
}

function onWindowResize() {

    camera.aspect = container.clientWidth / container.clientHeight;
    camera.updateProjectionMatrix();

    renderer.setSize(container.clientWidth, container.clientHeight);
    renderer.setPixelRatio(Math.min(window.devicePixelRatio, 2))

}



const UpdateDrillingState = () => {
    if (Math.abs(rotarySpeed) > Math.E) {
        isDrilling = true
    }
    else {
        isDrilling = false
    }
}

const StopDrill = () => {
    isDrilling = false
    rotarySpeed = 0
}


const SlowDrill = () => {
    rotarySpeed -= rotaryAcceleration
    UpdateDrillingState()

}
const SpeedDrill = () => {
    rotarySpeed += rotaryAcceleration
    UpdateDrillingState()

}

const SpeedElevator = () => {
    drillSpeed += drillAcceleration
}
const SlowElevator = () => {
    drillSpeed -= drillAcceleration
}

const CheckSilipsSit = () => {
    return isSilipsSitting;
}

const DrillRotary = (rpm) => {
    if (!isDrilling) return;

    if (!isKellyBushingOnRotaryTable) return;

    // Calculate rotation based on RPM
    const rotation = ((2 * Math.PI * rpm) / 60) * deltaTime;

    rotary_table.rotation.y += rotation;
    kelly_bushing.rotation.y += rotation;
};

const toggleSilipsSitStand = () => {
    if (isSilipsSitting) {
        // Silips is currently sitting, make it stand
        debugObject.silips_stand();
    } else {
        // Silips is currently standing, make it sit
        debugObject.silips_sit();
    }

    // Toggle the sit/stand state
    isSilipsSitting = !isSilipsSitting;
};

const toggleSwivelState = () => {
    if (isSwivelLatchedToPipes) {
        // If swivel is latched to pipes, unlatch it
        debugObject.tong_open()
        threeObject.unlatchSwivelToPipe()
        return 'opend';
    } else {
        // If swivel is not latched to pipes, latch it
        debugObject.tong_close()
        threeObject.latchSwivelToPipe()
        return 'closed';
    }


    // if (tongLeftHitInfo.length > 0) {
    //     debugObject.tong_close()
    //     threeObject.latchSwivelToPipe()
    // }
    // else if (tongRightHitInfo.length > 0) {
    //     debugObject.tong_open()
    //     threeObject.unlatchSwivelToPipe()
    // }
};


const threeObject = {}
threeObject.latchSwivel = LatchSwivel
threeObject.unlatchSwivel = UnlatchSwivel
threeObject.latchSwivelToPipe = LatchSwivelToPipes
threeObject.unlatchSwivelToPipe = UnlatchSwivelFromPipes
threeObject.accelerateUp = AccelerateUp
threeObject.accelerateDown = AccelerateDown
threeObject.addDrillPipe = AddDrillPipe
threeObject.drill = Drill
threeObject.speedDrill = SpeedDrill
threeObject.slowDrill = SlowDrill
threeObject.elevatorUp = SpeedElevator
threeObject.elevatorDown = SlowElevator
threeObject.stopDrill = StopDrill
threeObject.checkSilipsSit = CheckSilipsSit
threeObject.moveDrawworksCable = MoveDrawworksCable
threeObject.accelerateDownBase = AccelerateDownBase
threeObject.drillRotary = DrillRotary
threeObject.toggleSilipsSitStand = toggleSilipsSitStand;
threeObject.toggleSwivelState = toggleSwivelState;
window.ThreeObject = threeObject

