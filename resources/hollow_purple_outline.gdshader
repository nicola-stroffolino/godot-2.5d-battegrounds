shader_type spatial;
render_mode unshaded, cull_disabled;

uniform sampler2D TEXTURE;
uniform sampler2D FRAME;

uniform sampler2D NOISE;
uniform vec2 noise_scale = vec2(1.0, 1.0); // Scale for the noise texture
uniform vec2 distortion_strength = vec2(0.1, 0.1); // Strength of the distortion
uniform float time_factor = 1.0; // Factor to animate the noise over time

uniform vec2 offset;
uniform vec3 color_1: source_color = vec3(1.0, 0.0, 1.0);
uniform vec3 color_2: source_color = vec3(1.0, 0.0, 1.0);
uniform vec3 color_3: source_color = vec3(1.0, 0.0, 1.0);

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
	vec2 noise_uv = UV * noise_scale + vec2(TIME * time_factor, TIME * time_factor);

	vec4 noise_texture = texture(NOISE, noise_uv);
	vec4 color = texture(TEXTURE, panned_uv);
	vec4 inv_color = vec4(1.0 - color.rgb, color.a);
	vec4 alpha = texture(FRAME, UV + (noise_texture.rg - 0.5) * distortion_strength);
	
	color.rgb *= color_1;
	inv_color.rgb *= color_2;
	
    ALBEDO = color.rgb + inv_color.rgb;
    ALPHA = alpha.r * (inv_color.r + color.r);
}
