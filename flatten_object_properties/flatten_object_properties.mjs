import document_file_d_attente from '../document_for_file_d_attente.json' assert { type: 'json' };

function flatten_object_properties(object, key1, object_properties) {
  if (object === null || object === undefined) {
    return;
  }
  Object
    .keys(object)
    .forEach(key2 => {
      const property_path = [key1, key1 ? '.' : '', key2].join('')
      if (typeof object[key2] === 'string') {
        object_properties.push(property_path)
      } else {
        flatten_object_properties(object[key2], property_path, object_properties)
      }
    })
  return object_properties
}

console.clear()
const object_properties = flatten_object_properties(document_file_d_attente, '', [])
console.log(object_properties)