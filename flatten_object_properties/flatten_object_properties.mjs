import test_object from './test_object.json' assert { type: 'json' };

function flatten_object_properties(object, key1, object_properties) {
  if (!object || object !== Object(object)) {
    return;
  }
  return Object
    .keys(object)
    .forEach(key2 => {
      const keyPath = [key1, key1 ? '.' : '', key2].join('')
      console.log(keyPath)
      flatten_object_properties(object[key2], keyPath)
    });
}

console.clear()
flatten_object_properties(test_object, '')