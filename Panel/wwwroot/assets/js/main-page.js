var ropChartClass;
var pressChartClass;
var torChartClass;
var wobChartClass;

var ropChartItem;
var pressChartItem;
var torChartItem;
var wobChartItem;

var randomData = Array.from({length: 30}, () => Math.floor(Math.random() * 0));
var randomData;
let slider;

function handlerReleased(element, value) {
    restSliders();
    // DotNet.invokeMethodAsync( 'Panel16', 'DrawWorkHandlerReleased', pct);
}

function updateSlider(element) {
    if (element) {
        let parent = element.parentElement,
            lastValue = parent.getAttribute('data-slider-value');

        if (lastValue === element.value) {
            return; // No value change, no need to update then
        }

        // console.log(`updating slider form ${lastValue} to ${element.value}`);

        parent.setAttribute('data-slider-value', element.value);
        let $thumb = parent.querySelector('.range-slider__thumb'),
            $bar = parent.querySelector('.range-slider__bar'),
            $shadow = parent.querySelector('.range-slider__thumb__shadow'),
            pct = element.value * ((parent.clientHeight - $thumb.clientHeight) / parent.clientHeight);

        //console.log($shadow.style.bottom)
        $thumb.style.bottom = `${pct}%`;
        if ($shadow.style.bottom === '')
            $shadow.style.bottom = `${pct}%`;
        $bar.style.height = `calc(${pct}% + ${$thumb.clientHeight / 2}px)`;
        // $thumb.textContent = `${element.value}%`;

        // call dotnet to update DW status
        // DotNet.invokeMethodAsync('Panel16', 'SetDrawWorkHandlerStatus', element.value / 100);
        DotNet.invokeMethodAsync('Panel16', 'SetDrawWorkHandlerStatus', Number(element.value) );
    }
}

function restSliders(){
    const inputs = [].slice.call(document.querySelectorAll('.range-slider input'));
    inputs.forEach(input => input.value = '50');
    inputs.forEach(input => updateSlider(input));
}


function initSliderHandler(id) {
    const inputs = [].slice.call(document.querySelectorAll('.range-slider input'));
    inputs.forEach(input => input.setAttribute('value', '50'));
    inputs.forEach(input => updateSlider(input));
    
    // inputs.forEach(input => input.addEventListener('touchleave', element =>  handlerReleased(input)));
    inputs.forEach(input => input.addEventListener('touchend', element =>  handlerReleased(input)));
    inputs.forEach(input => input.addEventListener('mouseup', element =>  handlerReleased(input)));
    inputs.forEach(input => input.addEventListener('mousedown', _ => console.log('mouse down --handler')));
    inputs.forEach(input => input.addEventListener('input', element => updateSlider(input)));
    inputs.forEach(input => input.addEventListener('change', element => updateSlider(input)));
}


function setVerticalGaugeValue(id, value, ub, lb) {
    let root = document.getElementById(id);
    let arrow = root.getElementsByClassName('arrow')[0];
    let upperBound = root.getElementsByClassName('ub')[0];
    let lowerBound = root.getElementsByClassName('lb')[0];

    arrow.style.top = (value) + '%';
    upperBound.style.top = (ub) + '%';
    lowerBound.style.top = (lb) + '%';
}

function preset() {

    ropChartItem = document.querySelector('.chart-item.item-1');
    pressChartItem = document.querySelector('.chart-item.item-2');
    torChartItem = document.querySelector('.chart-item.item-3');
    wobChartItem = document.querySelector('.chart-item.item-4');
    initPreDataClass();

    ropChartClass.classList.remove('d-block')
    ropChartClass.classList.add('d-none')
    ropChartItem.classList.remove('active');
    pressChartClass.classList.remove('d-block')
    pressChartClass.classList.add('d-none')
    pressChartItem.classList.remove('active');
    torChartClass.classList.remove('d-block')
    torChartClass.classList.add('d-none')
    torChartItem.classList.remove('active');
    wobChartClass.classList.remove('d-block')
    wobChartClass.classList.add('d-none')
    wobChartItem.classList.remove('active');
}


const initPreDataClass = () => {
    ropChartClass = document.querySelector('#lineChartRop');
    pressChartClass = document.querySelector('#lineChartPress');
    torChartClass = document.querySelector('#lineChartTor');
    wobChartClass = document.querySelector('#lineChartWob');
}

function RopOn() {
    preset();

    ropChartClass.classList.add('d-block')
    ropChartItem.classList.add('active')
}

function PressOn() {
    preset();
    pressChartClass.classList.add('d-block')
    pressChartItem.classList.add('active')
}

function TorOn() {
    preset();
    torChartClass.classList.add('d-block')
    torChartItem.classList.add('active')
}

function WobOn() {
    preset();
    wobChartClass.classList.add('d-block')
    wobChartItem.classList.add('active')
}

function InitBop(){
    // Get all elements with the class 'bopbutton'
    var bopButtons = document.querySelectorAll('.bopbutton');

    // Add event listeners for hover
    bopButtons.forEach(function (button) {
        // Calculate the center of the button
        var bbox = button.getBBox();
        var centerX = bbox.x + bbox.width / 2;
        var centerY = bbox.y + bbox.height / 2;

        // Set the transform origin to the center of the button
        button.style.transformOrigin = centerX + 'px ' + centerY + 'px';

        button.addEventListener('mouseenter', function () {
            // Scale the element on hover
            this.style.transform = 'scale(1.1)';
        });

        button.addEventListener('mouseleave', function () {
            // Reset the scale on mouse leave
            this.style.transform = 'scale(1)';
        });

        button.addEventListener('click', function () {
            // Reset the scale on mouse leave
            this.style.transform = 'scale(0.9)';
            setTimeout(()=>{
                this.style.transform = 'scale(1)';
            },300)
            // this.classList.toggle('show')
        });
    });
}


/*main page chart setup*/

let chartDict = {}
let chartCanvasDict = {}

const killChart = (name) => {
    chartDict[name] = null;
    chartCanvasDict[name] = null;
}

function initChart(identity , type) {
    chartCanvasDict[identity] = document.getElementById(identity).getContext(type);
}

function getMainPageChartName(num){
    return num === 0 ? "Rop" : num === 1 ? "Press" : num === 2 ? "Torque" : num === 3 ? "Wob" : "UNK";
}


// arr = [] , labels = []
const updateChart = (identity , arr , labels , chartId , contextType = '2d' ) => {

    // console.log("updateChokeChart called from dotnet ");

    let ch =  chartDict[identity];

    // Check if chart instance exists
    if (!ch) {
        initChart(identity , contextType);
        let canvas = chartCanvasDict[identity];

        var data = {
            labels: labels, // Labels from 0 to 100
            datasets: [{
                label: getMainPageChartName(chartId),
                data: arr ,
                borderColor: 'blue',
                backgroundColor: 'transparent',
                borderWidth: 1,
            }]
        };

        // Define chart options
        // Define chart options
        var options = {
            responsive: true,
            maintainAspectRatio: false,
            pointLabels: false,
            plugins: {
                legend: {
                    display: false // Hide the legends
                }
            },
            scales: {
                x: {
                    min: 0, // Minimum value on the x-axis
                    max: 100, // Maximum value on the x-axis
                    stepSize: 1 // Step size for the x-axis
                },
                y: {

                }
            }
        };

        // Create a new line chart instance
        ch = new Chart(canvas, {
            type: 'line',
            data: data,
            options: options
        });

        chartDict[identity] = ch;
    } else {
        // console.log("updateChokeChart called updating dataset from dotnet ");
        // Update existing chart with new data
        ch.data.datasets[0].data = arr;
        ch.data.labels = labels;

        // console.log(ch.data);
        ch.update(); // Update the chart
    }
}

function setPipesEventListeners(){
    console.log('setPipesEventListeners called from js ');
    
    // Get the SVG element by its ID
    var svg = document.getElementById('Layer_1');

// Find the image within the SVG by its ID
    var standpipe = svg.getElementById('standpipe');
    var choke = svg.getElementById('choke');
// Add an onclick event listener to the image
    standpipe.addEventListener('click', function() {
        console.log("stand pipe clicked from js");
        DotNet.invokeMethodAsync('Panel16', 'InvokePipesEvent', 'standpipe' );
    });
    
    // Add an onclick event listener to the image
    choke.addEventListener('click', function() {
        console.log("choke clicked from js");
        DotNet.invokeMethodAsync('Panel16', 'InvokePipesEvent', 'chock' );
    });
}


const charts = {}
charts.main = updateChart;

window.Charts = charts;

window.resetHandlers = restSliders;
window.setVerticalGaugeValue = setVerticalGaugeValue;
window.initSliderHandler = initSliderHandler;
window.iniBop = InitBop;
window.initPipeEvents = setPipesEventListeners;
window.killChart = killChart;