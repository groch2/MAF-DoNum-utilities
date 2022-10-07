import cotes from './cotes.json' assert { type: 'json' }
import familles from './familles.json' assert { type: 'json' }
import types_doc from './types_doc.json' assert { type: 'json' }
import document from './document.json' assert { type: 'json' }
// Object.fromEntries(Object.entries(document).filter(([key, _]) => /id$/i.test(key)))
const properties = new Set(['coteDocumentId', 'familleDocumentId', 'typeDocumentId', 'numeroGc', 'libelle', 'commentaire', 'dateDocument'])
const _document = Object.fromEntries(Object.entries(document).filter(([key, _]) => properties.has(key)))
console.log(_document)
const { libelle: familleDocumentLibelle } = familles.find(({ familleDocumentId }) => _document.familleDocumentId === familleDocumentId)
const { libelle: coteDocumentLibelle } = cotes.value.find(({ coteDocumentId, familleDocumentId }) => coteDocumentId === _document.coteDocumentId && familleDocumentId === _document.familleDocumentId)
const { libelle: typeDocumentLibelle } = types_doc.value.find(({ typeDocumentId }) => typeDocumentId === _document.typeDocumentId)
console.log({ familleDocumentLibelle, coteDocumentLibelle, typeDocumentLibelle })