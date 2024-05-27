shader_type spatial;
render_mode unshaded, cull_disabled;

uniform sampler2D TEXTURE;
uniform float zoom = 1;
uniform float repeat = 1;

void vertex() {
	// Called for every vertex the material is visible on.
}

vec2 polar_coordinates(vec2 uv, vec2 center, float z, float r) {
	vec2 dir = uv - center;
	float radius = length(dir) * 2.0;
	float angle = atan(dir.y, dir.x) * 1.0/(3.1416 * 2.0);
	return mod(vec2(radius * z, angle * r), 1.0);
}

void fragment() {
	vec2 polar_uv = polar_coordinates(UV.xy, vec2(0.5), zoom, repeat);
	ALBEDO = texture(TEXTURE, polar_uv).rgb;
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}
