export function areStringsEqualsCaseInsensitive(a, b) {
  return a?.localeCompare(b, undefined, { sensitivity: 'accent' }) === 0;
}
