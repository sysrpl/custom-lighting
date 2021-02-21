use <standoffs.scad>;

$fn = 32;
dxf = "holders.dxf";

spacex = 41;
spacey = 42;

module holder() {
    rotate([0, 180, 0])
    translate([-38.66, 0, -5.5])
    intersection () {
        translate([0, 0, 5.5])
        rotate([0, 90, 0])
        translate([-50, 0, 0])
        linear_extrude(height = 50) 
        import(dxf, layer = "SIDE");
        linear_extrude(height = 30) 
        import(dxf, layer = "TOP");
    }
}

module group(count) {
    for (x = [0: count - 1]) {
        translate([spacex * x, 0, 0])
        holder();
    }

    for (x = [0: count - 1]) {
        translate([spacex * x + 47, spacey + 5, 0])
        rotate([0, 0, 180])
        holder();
    }
}

group(3);
translate([-spacex / 2, spacey + 8, 0])
group(4);
translate([0, spacey * 2 + 16, 0])
group(3);
