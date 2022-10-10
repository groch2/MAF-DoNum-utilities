import familles from './tryptiques/familles.json' assert { type: 'json' }
import cotes from './tryptiques/cotes.json' assert { type: 'json' }
import types from './tryptiques/types.json' assert { type: 'json' }
import { areStringsEqualsCaseInsensitive } from '../string_compare.mjs'

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
    .sort(({ famille_1, cote_1, type_1 }, { famille_2, cote_2, type_2 }) => {
      const famille_comparison = areStringsEqualsCaseInsensitive(famille_1, famille_2)
      if (famille_comparison !== 0) {
        famille_comparison;
      }
      const cote_comparison = areStringsEqualsCaseInsensitive(cote_1, cote_2)
      if (cote_comparison !== 0) {
        cote_comparison;
      }
      const type_comparison = areStringsEqualsCaseInsensitive(type_1, type_2)
      if (type_comparison !== 0) {
        type_comparison;
      }
    })
// console.log(tryptiques)
// process.exit(0)
const tryptiques_samples = ['DOCUMENTS COCOON', 'DOCUMENTS CONTRAT', 'DOCUMENTS EMOA'].map(famille_sample =>
  tryptiques.find(({ famille }) => areStringsEqualsCaseInsensitive(famille, famille_sample)))
console.log(tryptiques_samples)