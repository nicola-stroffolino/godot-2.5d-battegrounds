shader_type spatial;
render_mode unshaded, cull_disabled;

uniform sampler2D TEXTURE;
uniform sampler2D FRAME;
uniform vec2 offset;
uniform vec3 color_1: source_color = vec3(1.0, 0.0, 1.0);
uniform vec3 color_2: source_color = vec3(1.0, 0.0, 1.0);
//uniform vec3 color_3: source_color = vec3(1.0, 0.0, 1.0);
//uniform vec3 color_4: source_color = vec3(1.0, 0.0, 1.0);
//uniform vec2 core_scale = vec2(1.0, 1.0);

void vertex() {
	MODELVIEW_MATRIX = VIEW_MATRIX * mat4(INV_VIEW_MATRIX[0], INV_VIEW_MATRIX[1], INV_VIEW_MATRIX[2], MODEL_MATRIX[3]);
}

vec2 polar_coordinates(vec2 uv, vec2 center, float zoom, float range) {
	vec2 dir = uv - center;
	float radius = length(dir) * 2.0;
	float angle = (atan(dir.y, dir.x) / TAU) + 0.5; //* 1.0/(3.1416 * 2.0);
	return mod(vec2(radius * zoom, angle * range), 1.0);
}

vec2 scale_uv(vec2 uv, vec2 scale) {
	vec2 scaled_uv = uv * scale;
	vec2 center_offset = (scale / 2.0) - 0.5;
	scaled_uv -= center_offset;
	return clamp(scaled_uv, vec2(0.0), vec2(1.0));
}

void fragment() {
	vec2 polar_uv = polar_coordinates(UV.xy, vec2(0.5), 1.0, 1.0);
	vec2 panned_uv = polar_uv + vec2(offset.x * TIME, offset.y * TIME);
	
	vec4 tex_color = texture(TEXTURE, panned_uv);
	tex_color.rgb *= color_1;
	vec4 inv_tex_color = vec4(1.0 - texture(TEXTURE, panned_uv).rgb, 1.0);
	inv_tex_color.rgb *= color_2;
	
	vec4 frame = texture(FRAME, UV);
	
	ALBEDO = (tex_color.rgb + inv_tex_color.rgb) * frame.rgb;
	ALPHA = frame.r;
}