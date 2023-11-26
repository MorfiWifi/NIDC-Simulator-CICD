window.methods3d = {
    cubes: null,
    scene: null,
    camera: null,
    renderer: null,
    CreateScene: function () {
        this.scene = new THREE.Scene();
        this.camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
        this.renderer = new THREE.WebGLRenderer();
        this.renderer.setSize(window.innerWidth, window.innerHeight);
        document.body.appendChild(this.renderer.domElement);
        this.CreateBox()
    },
    CreateBox: function () {
        const geometry = new THREE.BoxGeometry();
        const material = new THREE.MeshBasicMaterial({ color: 0x00ff00, transparent: true,opacity:0.5 });
        this.cube = new THREE.Mesh(geometry, material);
        this.scene.add(this.cube);

        this.camera.position.z = 10;
    },
    Animate: function () {        
        this.renderer.render(this.scene, this.camera);
    },
    LittleAnimation: function (angle) {
        this.cube.rotation.x += angle*10;
        this.cube.rotation.y += angle * 10;
        this.cube.rotation.z += angle * 10;        
        this.Animate()       
    }

}