import familles from './familles.json' assert { type: 'json' }
import cotes from './cotes.json' assert { type: 'json' }
import types from './types.json' assert { type: 'json' }
import are_strings_equals_case_insensitive from './strings_compare.mjs'
import * as fs from 'fs/promises'

export function localCompareCaseInsensitive(a, b) {
  return a?.localeCompare(b, undefined, { sensitivity: 'accent' });
}

const wanted_familles = ['DOCUMENTS CONTRAT', 'DOCUMENTS EMOA']
const donum_valid_tryptiques =
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
    .filter(({ famille }) => wanted_familles.findIndex(f => are_strings_equals_case_insensitive(famille, f)) >= 0)
console.log(donum_valid_tryptiques)
await fs.writeFile('C:/Users/deschaseauxr/Documents/Donum/tryptiques/tryptiques.json', JSON.stringify(donum_valid_tryptiques, undefined, 2))
process.exit(0)
