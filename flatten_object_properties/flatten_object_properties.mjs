// import document_file_d_attente from '../document_for_file_d_attente.json' assert { type: 'json' }
import test_object from './test_object.json' assert { type: 'json' }
import { compare_strings_case_insensitive } from '../strings_compare.mjs'
const wanted_types = new Set(['boolean', 'number', 'string'])
function flatten_object_properties(object, key_1) {
  return Object
    .keys(object)
    .flatMap(key_2 => {
      const property_path = [key_1, key_1 ? '.' : '', key_2].join('')
      const _object = object[key_2]
      return wanted_types.has(typeof _object) || _object === null ?
        [property_path] :
        _object !== undefined ?
          flatten_object_properties(_object, property_path) : []
    })
}
console.clear()
const object_properties =
  // flatten_object_properties(document_file_d_attente, null)
  flatten_object_properties(test_object, null)
    .sort((a, b) => compare_strings_case_insensitive(a, b))
    .join('\n')
console.log(object_properties)