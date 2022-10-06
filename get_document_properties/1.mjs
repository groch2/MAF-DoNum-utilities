import cotes from './cotes.json' assert { type: 'json' }
import familles from './familles.json' assert { type: 'json' }
import types_doc from './types_doc.json' assert { type: 'json' }
import { areStringsEqualsCaseInsensitive } from '../string_compare.mjs'

const famille_text = 'Documents contrat'
const { familleDocumentId } = familles.find(({ libelle }) => areStringsEqualsCaseInsensitive(libelle, famille_text))

const cote_text = 'Contrats'
const { coteDocumentId } = cotes.value.find(({ libelle, familleDocumentId: _familleDocumentId }) => areStringsEqualsCaseInsensitive(libelle, cote_text) && _familleDocumentId == familleDocumentId)

const type_text = 'Avenant contrat'
const { typeDocumentId } = types_doc.value.find(({ libelle }) => areStringsEqualsCaseInsensitive(libelle, type_text))

console.log({ familleDocumentId, coteDocumentId, typeDocumentId })