use <standoffs.scad>;

$fn = 128;
dxf = "lights-power.dxf";

case_height = 35;
case_length = 71;
enable_top = 1;
enable_bottom = 1;

face_move = -10;
power_move = -5;

module bottom() {
    difference() {
        union() {
            linear_extrude(height = 2) 
            import(dxf, layer = "BOTTOM");
            linear_extrude(height = case_height) 
            import(dxf, layer = "WALLS");
            linear_extrude(height = case_height) 
            import(dxf, layer = "POSTS");
        }
        // grill
        for (i = [0 : 10])
        hull() {
            translate([30 + i * 5 + face_move, -3, 6]) {
                sphere(1, $fn = 16);
                translate([5, 0, 18])
                sphere(1, $fn = 16);
                translate([0, 80, 0])
                sphere(1, $fn = 16);
                translate([5, 80, 18])
                sphere(1, $fn = 16);
            }
        }
        // barrel jack
        translate([-1 + face_move, 52, case_height / 2])
        rotate([0, 90, 0])
        cylinder(r = 12 / 2, h = 10);
        // led
        translate([-1 + face_move, 39.5, case_height / 2])
        rotate([0, 90, 0])
        cylinder(r = 2.5, h = 10);
        // on / off switch
        translate([-1 + face_move, 22, case_height / 2])
        rotate([0, 90, 0])
        cylinder(r = 21.5 / 2, h = 10);
        // output hole
        translate([145, case_length / 2  - 1, case_height / 2 + 3])
        rotate([0, 90, 0])
        cylinder(r = 3, h = 10);
        // SD car slot
        translate([0, 0, -0.01])
        linear_extrude(height = 7.5) 
        import(dxf, layer = "SD_CARD");
        // lid holes
        translate([5 + face_move, 5, case_height])
        standoffDrill(5, 1);
        translate([5 + face_move, 66, case_height])
        standoffDrill(5, 1);
        translate([147.5, 66, case_height])
        standoffDrill(5, 1);
        translate([147.5, 5, case_height])
        standoffDrill(5, 1);
    }
    linear_extrude(height = 2) 
    import(dxf, layer = "FEET");
    // power converter
    translate([32.5 + power_move, 15, 2])
    standoff(5, 2.5);
    translate([32.5 + power_move, 56, 2])
    standoff(5, 2.5);
    translate([58 + power_move, 63.5, 2])
    standoff(5, 2.5);
    translate([58 + power_move, 7.5, 2])
    standoff(5, 2.5);
    // raspberry pi
    translate([88.5, 21.1, 2])
    standoff(5, 2.5);
    translate([88.5, 44.1, 2])
    standoff(5, 2.5);
    translate([146.5, 44.1, 2])
    standoff(5, 5);
    translate([146.5, 18.5, 2])
    standoff(5, 2.5);
}

module top() {
    difference() {
        linear_extrude(height = 2) 
        import(dxf, layer = "BOTTOM");
        translate([5 + face_move, 5, 2.4])
        countersink(4);
        translate([5 + face_move, 66, 2.4])
        countersink(4);
        translate([147.5, 66, 2.4])
        countersink(4);
        translate([147.5, 5, 2.4])
        countersink(4);
    }
    linear_extrude(height = 3) 
    import(dxf, layer = "LOGO");
}

if (enable_bottom > 0)
    bottom();
if (enable_top > 0)
    translate([0, -85, 0])
    top();