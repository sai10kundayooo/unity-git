#! /usr/bin/env python
# -*- coding: utf-8 -*-
#
# ShadeToUnity.py
# Copyright 2012 e frontier, Inc. All Rights Reserved.
#

#import sys # sys imported by default.
import os
#import shutil

class Settings:
	fixname = True

scene = xshade.scene()

def is_ascii (string):
    if string:
        return max([ord(char) for char in string]) < 128
    return True
def is_windows ():
	import platform
	return platform.system() == 'Windows'

names = []
names_index = 0
def is_unique_name (shape):
	global names
	if shape.name == None:
		return False
	if shape.name in names:
		return False
	names.append(shape.name)
	return True
def make_unique_name (shape):
	global names
	global names_index
	if not is_unique_name(shape):
		oldname = shape.name
		while not is_unique_name(shape):
			shape.name = shape.name + '_%d' % names_index
			names_index = names_index + 1
		names.append(shape.name)
		sys.stdout.write("Shade 3D Loader: Warning: '%s' is not unique name, using unique name '%s' instead.\n" % (oldname, shape.name))
def must_unique_name (shape):
	if shape.is_container:
		if 2 <= shape.part_type: return True
	return False
def get_symbols (shape):
	symbols = []
	for c in shade.special_characters:
		if shape.has_symbol(ord(c)):
			symbols.append(c)
	return symbols
def default_name(shape):
	if shape.is_container:
		if shape.part_type == 0: return 'Part'
		if shape.part_type == 1: return 'Surface Part'
		if shape.part_type == 2: return 'Rotator Joint'
		if shape.part_type == 3: return 'Slider Joint'
		if shape.part_type == 4: return 'Scale Joint'
		if shape.part_type == 5: return 'Uniscale Joint'
		if shape.part_type == 6: return 'Light Effector'
		if shape.part_type == 7: return 'Path Joint'
		if shape.part_type == 8: return 'Morph Joint'
		if shape.part_type == 9: return 'Custom Joint'
		if shape.part_type == 10: return 'Ball Joint'
		if shape.part_type == 11: return 'Camera'
		if shape.part_type == 12: return 'Sound'
		if shape.part_type == 13: return 'Switch Effector'
		if shape.part_type == 14: return 'Path Replicator'
		if shape.part_type == 15: return 'Surface Replicator'
		if shape.part_type == 16: return 'Bone Joint'
		if shape.part_type == 100: return 'Master Surface Part'
		if shape.part_type == 101: return 'Link'
		if shape.part_type == 102: return 'Image Part'
		if shape.part_type == 103: return 'Master Shape Part'
		if shape.part_type == 104: return 'Master Shape Object'
		if shape.part_type == 105: return 'Local Axis'
	if shape.type == 0: return 'Sentinel'
	if shape.type == 1: return 'Unused'
	if shape.type == 2: return 'Unknown Part'
	if shape.type == 3:
		if shape.spotlight: return 'Spot Light'
		if shape.distribution_type == 2: return 'Distribution Light'
		if shape.attenuation == 0: return 'Ambient Light'
		return 'Point Light'
	if shape.type == 4:
		if 0 < shape.light_intensity:
			if shape.light_type == 0: return 'Area Light'
			if shape.light_type == 1: return 'Linear Light'
			return 'Unknown Light'
		if shape.closed:
			if shape.is_extruded: return 'Extruded Closed Line'
			if shape.is_revolved: return 'Revolved Closed Line'
			return 'Closed Line'
		if shape.is_extruded: return 'Extruded Open Line'
		if shape.is_revolved: return 'Revolved Open Line'
		return 'Open Line'
	if shape.type == 5: return 'Sphere'
	if shape.type == 6:
		if shape.is_extruded: return 'Extruded Disk'
		if shape.is_revolved: return 'Revolved Disk'
		return 'Disk'
	if shape.type == 7: return 'Polygon Mesh'
	if shape.type == 8: return 'Master Surface'
	if shape.type == 9: return 'Unknown'
	if shape.type == 10: return 'Master Image'
	assert False
	return 'Unknown'

def validate_name (shape):
	if not is_ascii(shape.name):
		if Settings.fixname:
			symbols = get_symbols(shape)
			sys.stdout.write("Shade 3D Loader: Warning: '%s' has non ascii characters, using default name instead.\n" % shape.name)
			shape.name = None
			if not is_ascii(shape.name):
				shape.name = default_name(shape)
			for symbol in symbols:
				shape.name = symbol + shape.name
	if must_unique_name(shape):
		make_unique_name(shape)
	if shape.has_son:
		validate_name(shape.son.bro)
	if shape.has_bro:
		validate_name(shape.bro)
validate_name(scene.shape.son.bro)

def get_output_filepath ():
	if 'SHADETOUNITY_OUTPUT_PATH' in os.environ:
		if is_windows(): return unicode(os.environ['SHADETOUNITY_OUTPUT_PATH'], 'mbcs').replace('/', '\\')
		return os.environ['SHADETOUNITY_OUTPUT_PATH']
	assert False , 'SHADETOUNITY_OUTPUT_PATH not found.'
	return None

def encode (s):
	if is_windows(): return s.encode('utf-8')
	return s

output_filepath = get_output_filepath()
#print encode(os.path.basename(output_filepath))

if os.path.exists(output_filepath):
	os.remove(output_filepath)
parentdir = os.path.abspath(os.path.join(output_filepath, os.path.pardir))
if not os.path.exists(parentdir):
	os.makedirs(parentdir)
scene.save_FBX(encode(output_filepath))
scene.dirty = False
xshade.quit()
