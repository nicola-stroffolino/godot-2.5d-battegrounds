shader_type spatial;
render_mode unshaded, cull_disabled;

uniform sampler2D TEXTURE;
uniform sampler2D FRAME;
uniform sampler2D CORE;
uniform vec2 offset;
uniform vec3 color_1: source_color = vec3(1.0, 0.0, 1.0);
uniform vec3 color_2: source_color = vec3(1.0, 0.0, 1.0);
uniform vec3 color_3: source_color = vec3(1.0, 0.0, 1.0);
uniform vec2 core_scale = vec2(1.0, 1.0);

void vertex() {
	MODELVIEW_MATRIX = VIEW_MATRIX * mat4(INV_VIEW_MATRIX[0], INV_VIEW_MATRIX[1], INV_VIEW_MATRIX[2], MODEL_MATRIX[3]);
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
	
	vec2 scaled_uv = UV * core_scale;
	//vec2 center_offset = (scaled_uv - UV) * 0.5;
	
	//scaled_uv -= 0.5; // scale == 2
	//scaled_uv -= 1.5; // scale == 4
	//scaled_uv -= 3.5; // scale == 8
	vec4 core = texture(CORE, clamp(scaled_uv, vec2(0.0), vec2(1.0)));
	
	color.rgb *= color_1;
	inv_color.rgb *= color_2;
	core.rgb *= color_3;
	
	ALBEDO = color.rgb;
	ALBEDO += inv_color.rgb;
	ALBEDO += core.rgb;
	ALPHA = texture(FRAME, UV).r;// * (inv_color.r + color.r + core.r);
}