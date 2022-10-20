// import document_file_d_attente from '../document_for_file_d_attente.json' assert { type: 'json' };
import test_object from './test_object.json' assert { type: 'json' };

const wanted_types = new Set(['boolean', 'number', 'string'])
function flatten_object_properties(object, key1) {
  return Object
    .keys(object)
    .flatMap(key2 => {
      const property_path = [key1, key1 ? '.' : '', key2].join('')
      const _object = object[key2]
      return wanted_types.has(typeof _object) || _object === null ?
        [property_path] :
        _object !== undefined ?
          flatten_object_properties(_object, property_path) : []
    })
}

console.clear()
// const object_properties = flatten_object_properties(document_file_d_attente, '')
const object_properties = flatten_object_properties(test_object, '')
console.log(object_properties)