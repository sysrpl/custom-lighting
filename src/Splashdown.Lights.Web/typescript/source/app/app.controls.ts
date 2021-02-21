interface Controls {
    updating: boolean;
    color1: HTMLInputElement;
    color2: HTMLInputElement;
    color3: HTMLInputElement;
    speed: Slider;
    length: Slider;
    brightness: Slider;
    saturation: Slider;
}

let controls: Controls = {
    updating: true,
    color1: null,
    color2: null,
    color3: null,
    speed: null,
    length: null,
    brightness: null,
    saturation: null
};

function initControls() {
    if (initControls["__init"])
        return;
    initControls["__init"] = true;

    let timer: number = 0;
    let timeout = 50;
    controls.color1 = get("#color1") as HTMLInputElement;
    controls.color2 = get("#color2") as HTMLInputElement;
    controls.color3 = get("#color3") as HTMLInputElement;
    for (let i = 1; i < 4; i++)
        get(`#color${i}`).addEventListener("input", e => {
            if (controls.updating)
                return;
            if (timer != 0)
                clearTimeout(timer);
            timer = setTimeout(() => {
                timer = 0;
                let input = e.target as HTMLInputElement;
                postWebRequest("/?method=setcolor", { value: input.value, index: i });
            }, timeout);
        });
    controls.speed = new Slider("#speed .slider", "#speed .associate");
    controls.speed.step = 0.1;
    controls.speed.min = -5;
    controls.speed.max = 5;
    controls.speed.position = 1;
    controls.speed.method = "/?method=setspeed";
    controls.length = new Slider("#length .slider", "#length .associate");
    controls.length.step = 25;
    controls.length.min = 25;
    controls.length.max = 1000;
    controls.length.position = 100;
    controls.length.method = "/?method=setlength";
    controls.length.divisor = 100;
    controls.brightness = new Slider("#brightness .slider", "#brightness .associate");
    controls.brightness.step = 1;
    controls.brightness.min = 0;
    controls.brightness.max = 100;
    controls.brightness.position = 100;
    controls.brightness.method = "/?method=setbrightness";
    controls.brightness.divisor = 100;
    controls.saturation = new Slider("#saturation .slider", "#saturation .associate");
    controls.saturation.step = 1;
    controls.saturation.min = 0;
    controls.saturation.max = 100;
    controls.saturation.position = 100;
    controls.saturation.method = "/?method=setsaturation";
    controls.saturation.divisor = 100;

    function sliderChange(slider: Slider) {
        if (controls.updating)
            return;
        if (timer != 0)
            clearTimeout(timer);
        timer = setTimeout(() => {
            timer = 0;
            postWebRequest(slider.method, { value: slider.position / slider.divisor });
        }, timeout);
    }

    controls.speed.onchange = sliderChange;
    controls.length.onchange = sliderChange;
    controls.brightness.onchange = sliderChange;
    controls.saturation.onchange = sliderChange;
}