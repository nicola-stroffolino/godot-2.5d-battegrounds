shader_type spatial;
render_mode unshaded, cull_disabled;

uniform sampler2D TEXTURE;
uniform sampler2D FRAME;
uniform sampler2D CORE;
uniform vec2 offset;
uniform vec3 color_1: source_color = vec3(1.0, 0.0, 1.0);
uniform vec3 color_2: source_color = vec3(1.0, 0.0, 1.0);
uniform vec3 color_3: source_color = vec3(1.0, 0.0, 1.0);

void vertex() {
	// Called for every vertex the material is visible on.
}

vec2 polar_coordinates(vec2 uv, vec2 center, float zoom, float range) {
	vec2 dir = uv - center;
	float radius = length(dir) * 2.0;
	float angle = (atan(dir.y, dir.x) / TAU) + 0.5; //* 1.0/(3.1416 * 2.0);
	return mod(vec2(radius * zoom, angle * range), 1.0);
}

void fragment() {
	vec2 polar_uv = polar_coordinates(UV.xy, vec2(0.5), 1.0, 1.0);
	
	vec2 panned_uv = polar_uv + vec2(offset.x * TIME, offset.y * TIME);
	vec2 inv_panned_uv = polar_uv + vec2(-1.0 * offset.x * TIME, offset.y * TIME);
	
	vec4 color = texture(TEXTURE, panned_uv);
	vec4 inv_color = vec4(1.0 - texture(TEXTURE, panned_uv).rgb, 1.0);
	vec4 core = texture(CORE, UV);
	
	color.rgb *= color_1;
	inv_color.rgb *= color_2;
	
	ALBEDO = color.rgb;
	ALBEDO += inv_color.rgb;
	ALBEDO += core.rgb;
	ALPHA = texture(FRAME, UV).r * (inv_color.r + color.r + core.r);
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}
