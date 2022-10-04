import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/documents.json' assert { type: 'json' };
const compteIdToKeep = new Set([17187, 19913, 72002354])
const documentsToKeep =
  json_object.value
    .filter(({ compteId }) => compteIdToKeep.has(compteId))
    .reduce((state, item) => state.findIndex(({ compteId }) => compteId === item.compteId) === -1 ? [item, ...state] : state, [])
    .map(({ docn }) => docn)
const documentsIdToKeep = new Set(documentsToKeep)
const documentsIdToDelete =
  json_object.value
    .filter(({ docn }) => !documentsIdToKeep.has(docn))
    .map(({ docn }) => docn)
console.log(documentsIdToDelete.map(v => `'${v}'`).join('\n'))