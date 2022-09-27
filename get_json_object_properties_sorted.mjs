import json_object from './document.json' assert { type: 'json' };
const properties = Object.keys(json_object).sort((a, b) => a.localeCompare(b, undefined, { sensitivity: 'accent' }))
console.log(properties.join('\r\n'))
