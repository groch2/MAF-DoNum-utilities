import test_object from './test_object.json' assert { type: 'json' }
import { compare_strings_case_insensitive } from '../strings_compare.mjs'

const wanted_primitive_types = new Set(['boolean', 'number', 'string'])
function flatten_object_properties(object, object_path) {
  return Object
    .keys(object)
    .flatMap(sub_object_property_key => {
      const property_path = [object_path, object_path ? '.' : '', sub_object_property_key].join('')
      const property_value = object[sub_object_property_key]
      return wanted_primitive_types.has(typeof property_value) || property_value === null ?
        [property_path] :
        property_value !== undefined ?
          flatten_object_properties(property_value, property_path) : []
    })
}

console.clear()
const object_properties =
  // flatten_object_properties(document_file_d_attente, null)
  flatten_object_properties(test_object, null)
    .sort((a, b) => compare_strings_case_insensitive(a, b))
    .join('\n')
console.log(object_properties)