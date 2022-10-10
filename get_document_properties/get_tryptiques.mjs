import familles from './tryptiques/familles.json' assert { type: 'json' }
import cotes from './tryptiques/cotes.json' assert { type: 'json' }
import types from './tryptiques/types.json' assert { type: 'json' }
import * as fs from 'fs/promises'

export function localCompareCaseInsensitive(a, b) {
  return a?.localeCompare(b, undefined, { sensitivity: 'accent' });
}

const tryptiques =
  types.value
    .flatMap(({ code: type_code, coteDocumentId: type_coteDocumentId }) =>
      cotes.value
        .filter(({ coteDocumentId }) => type_coteDocumentId === coteDocumentId)
        .flatMap(({ code: cote_code, familleDocumentId: cote_familleDocumentId }) =>
          familles.value
            .filter(({ familleDocumentId }) => cote_familleDocumentId === familleDocumentId)
            .flatMap(({ code: famille_code }) =>
              ({ famille: famille_code, cote: cote_code, type: type_code }))))
    .sort((
      { famille: famille_1, cote: cote_1, type: type_1 },
      { famille: famille_2, cote: cote_2, type: type_2 }) => {
      return localCompareCaseInsensitive(famille_1, famille_2) ||
        localCompareCaseInsensitive(cote_1, cote_2) ||
        localCompareCaseInsensitive(type_1, type_2)
    })
// console.log(tryptiques)
await fs.writeFile('C:/Users/deschaseauxr/Documents/DONUM/get_document_properties/tryptiques/tryptiques.json', JSON.stringify(tryptiques, undefined, 2))
process.exit(0)
const tryptiques_samples = ['DOCUMENTS COCOON', 'DOCUMENTS CONTRAT', 'DOCUMENTS EMOA'].map(famille_sample =>
  tryptiques.find(({ famille }) => areStringsEqualsCaseInsensitive(famille, famille_sample)))
console.log(tryptiques_samples)