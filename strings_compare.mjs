export const compare_strings_case_insensitive = (a, b) =>
  a?.localeCompare(b, undefined, { sensitivity: 'accent' })
export const are_strings_equals_case_insensitive = (a, b) =>
  compare_strings_case_insensitive(a, b) === 0
// are_strings_equals_case_insensitive