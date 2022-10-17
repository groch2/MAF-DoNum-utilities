import cotes from './cotes.json' assert { type: 'json' }
import familles from './familles.json' assert { type: 'json' }
import types_doc from './types.json' assert { type: 'json' }
import are_strings_equals_case_insensitive from './string_compare.mjs'

const famille_text = 'Documents contrat'
const { familleDocumentId } = familles.find(({ libelle }) => are_strings_equals_case_insensitive(libelle, famille_text))

const cote_text = 'Autres'
const { coteDocumentId } = cotes.value.find(({ libelle, familleDocumentId: _familleDocumentId }) => are_strings_equals_case_insensitive(libelle, cote_text) && _familleDocumentId == familleDocumentId)

const type_text = 'Divers'
const { typeDocumentId } = types_doc.value.find(({ libelle }) => are_strings_equals_case_insensitive(libelle, type_text))

console.log({ familleDocumentId, coteDocumentId, typeDocumentId })