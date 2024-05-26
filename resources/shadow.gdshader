//shader_type spatial;
//render_mode world_vertex_coords, unshaded;
//
//uniform vec4 albedo : source_color;
//uniform sampler2D texture_albedo : source_color;
//uniform float cube_half_size = 1.0;
//uniform float shadow_opacity : hint_range(0.0, 1.0) = 0.5;
//uniform float shadow_radius : hint_range(0.0, 1.0) = 0.5;
//
//uniform sampler2D DEPTH_TEXTURE: hint_depth_texture;
//
//varying mat4 INV_MODEL_MATRIX;
//
//void vertex(){
	//INV_MODEL_MATRIX = inverse(MODEL_MATRIX);
//}
//
//// Credit: https://stackoverflow.com/questions/32227283/getting-world-position-from-depth-buffer-value
//vec3 world_pos_from_depth(float depth, vec2 screen_uv, mat4 inverse_proj, mat4 inverse_view) {
	//float z = depth;
	//
	//vec4 clipSpacePosition = vec4(screen_uv * 2.0 - 1.0, z, 1.0);
	//vec4 viewSpacePosition = inverse_proj * clipSpacePosition;
	//
	//viewSpacePosition /= viewSpacePosition.w;
	//
	//vec4 worldSpacePosition = inverse_view * viewSpacePosition;
	//
	//return worldSpacePosition.xyz;
//}
//
//void fragment() {
	//float depth = texture(DEPTH_TEXTURE, SCREEN_UV).x;
	//vec3 world_pos = world_pos_from_depth(depth, SCREEN_UV, INV_PROJECTION_MATRIX, (INV_VIEW_MATRIX));
	//vec4 test_pos = (INV_MODEL_MATRIX * vec4(world_pos, 1.0));
	//
	//if (abs(test_pos.x) > cube_half_size ||abs(test_pos.y) > cube_half_size || abs(test_pos.z) > cube_half_size) {
		//discard;
	//}
	//
	////vec2 center = vec2(0.5, 0.5);
    ////float distance = length(UV - center);
    ////float alpha = smoothstep(shadow_radius, shadow_radius - 0.2, distance);
	//
	////COLOR = vec4(shadow_color, alpha);
	//
	//ALBEDO = texture(texture_albedo, (test_pos.xz) + 0.5).rgb * albedo.rgb;
//}


//shader_type canvas_item;
//render_mode skip_vertex_transform;
//
//uniform vec3 shadow_color : source_color = vec3(0.0, 0.0, 0.0);
//uniform float shadow_opacity : hint_range(0.0, 1.0) = 0.5;
//uniform float shadow_radius : hint_range(0.0, 1.0) = 0.5;
//uniform float effect_factor : hint_range(0, 1) = 0.3; 
//
//void vertex() {
	//VERTEX = (MODEL_MATRIX * vec4(VERTEX, 0.0, 1.0)).xy;
//}
//
//void fragment() {
    //vec2 center = vec2(0.5, 0.5);
    //float distance = length(UV - center);
    //float alpha = smoothstep(shadow_radius, shadow_radius - 0.2, distance);
	//
	//COLOR = vec4(shadow_color, alpha);
//}

shader_type spatial;
render_mode world_vertex_coords, unshaded;

uniform vec3 albedo : source_color;
uniform float cube_half_size = 1.0;
uniform float shadow_opacity : hint_range(0.0, 1.0) = 0.5;
uniform float shadow_radius = 0.5;
uniform float shadow_spread : hint_range(0.0, 1.0) = 0.5;

uniform sampler2D DEPTH_TEXTURE: hint_depth_texture;

varying mat4 INV_MODEL_MATRIX;
varying vec3 WORLD_POSITION;
varying float CUR_ALPHA;

void vertex(){
	INV_MODEL_MATRIX = inverse(MODEL_MATRIX);
	WORLD_POSITION = (INV_MODEL_MATRIX * vec4(0, 0, 0, 1)).xyz;
}

// Credit: https://stackoverflow.com/questions/32227283/getting-world-position-from-depth-buffer-value
vec3 world_pos_from_depth(float depth, vec2 screen_uv, mat4 inverse_proj, mat4 inverse_view) {
	float z = depth;
	
	vec4 clipSpacePosition = vec4(screen_uv * 2.0 - 1.0, z, 1.0);
	vec4 viewSpacePosition = inverse_proj * clipSpacePosition;
	
	viewSpacePosition /= viewSpacePosition.w;
	
	vec4 worldSpacePosition = inverse_view * viewSpacePosition;
	
	return worldSpacePosition.xyz;
}

void fragment() {
	float depth = texture(DEPTH_TEXTURE, SCREEN_UV).x;
	vec3 world_pos = world_pos_from_depth(depth, SCREEN_UV, INV_PROJECTION_MATRIX, INV_VIEW_MATRIX);
	vec4 test_pos = (INV_MODEL_MATRIX * vec4(world_pos, 1.0));
	
	if (abs(test_pos.x) > cube_half_size || abs(test_pos.y) > cube_half_size || abs(test_pos.z) > cube_half_size) {
		discard;
	}
	
	vec2 center = vec2(0.5, 0.5);
    float distance = length(test_pos.xz);
	
	float dest_alpha = shadow_radius - shadow_spread;
    if (dest_alpha == shadow_radius) dest_alpha -= 0.01; // if dest_alpha == shadow_radius weird thigs happen, lets not
	float alpha = smoothstep(shadow_radius, dest_alpha, distance) * shadow_opacity;
	
	ALBEDO = albedo;
	ALPHA = alpha;
}