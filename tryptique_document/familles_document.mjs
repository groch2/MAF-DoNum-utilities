import json_object from 'file://C:/Users/deschaseauxr/Documents/Donum/tryptique_document/familles_document.json' assert { type: 'json' };
const wanted_famille_doc_id = new Set([17, 19, 22, 23, 24])
const familles_codes =
  json_object.value
    .filter(({ isActif, familleDocumentId }) => isActif && wanted_famille_doc_id.has(familleDocumentId))
    .map(({ code }) => code).sort()
console.log({ familles_codes })