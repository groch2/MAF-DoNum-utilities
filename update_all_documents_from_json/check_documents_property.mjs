import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/documents.json' assert { type: 'json' };
var output = json_object.value.map(({ sens }) => sens ?? 'null').join('\n')
console.log(output)
