function uuidv4() {
  return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, (c) =>
    (
      +c ^
      (crypto.getRandomValues(new Uint8Array(1))[0] & (15 >> (+c / 4)))
    ).toString(16)
  );
}

function successMessage(message) {
  Notiflix.Report.success("Success!", message, "Ok");
}

function errorMessage(message) {
  Notiflix.Report.failure("Error!", message, "Ok");
}

function confirmMessage(message) {
  let confirmMessageResult = new Promise(function (success, error) {
    Notiflix.Confirm.show(
      "Confirm",
      message,
      "Yes",
      "No",
      function okCb() {
        success();
      },
      function cancelCb() {
        error();
      }
    );
  });
  return confirmMessageResult;
}
