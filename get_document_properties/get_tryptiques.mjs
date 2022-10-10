import familles from './tryptiques/familles.json' assert { type: 'json' }
import cotes from './tryptiques/cotes.json' assert { type: 'json' }
import types from './tryptiques/types.json' assert { type: 'json' }

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
console.log(tryptiques)