
//Функция удаления фильма по id
async function DeleteFilmById(id) {
	const response = await fetch("/api/films/" + id, {
		method: "DELETE",
		headers: { "Accept": "application/json" }
	});

	if (response.ok === true) {
		const film = await response.json();
		alert("Успешное удаление");
		document.querySelector("tr[data-rowid='" + film.id + "']").remove();
	}
}

//Функция для создания строки таблицы
function row(film) {
	const tr = document.createElement("tr");
	tr.setAttribute("data-rowid", film.id);

	const idTd = document.createElement("td");
	idTd.append(film.id);
	tr.append(idTd);

	const NameTd = document.createElement("td");
	NameTd.append(film.name);
	tr.append(NameTd);

	const GenreTd = document.createElement("td");
	GenreTd.append(film.genre.name);
	tr.append(GenreTd);

	const DurationTd = document.createElement("td");
	DurationTd.append(film.duration);
	tr.append(DurationTd)

	const FilmProductionTd = document.createElement("td");
	FilmProductionTd.append(film.filmProduction.name);
	tr.append(FilmProductionTd);

	const CountryProductionTd = document.createElement("td");
	CountryProductionTd.append(film.countryProduction.name);
	tr.append(CountryProductionTd);

	const AgeLimitTd = document.createElement("td");
	AgeLimitTd.append(film.ageLimit);
	tr.append(AgeLimitTd);

	const DescriptionTd = document.createElement("td");
	DescriptionTd.append(film.description);
	tr.append(DescriptionTd);

	const linksTd = document.createElement("td");

	const linkForEdit = document.createElement("a");
	linkForEdit.setAttribute("film-id", film.id);
	linkForEdit.append("Изменить")
	linkForEdit.addEventListener("click", event => {
		event.preventDefault();
		localStorage.setItem('id', film.id);
		window.location = "edit.html";
	});
	linksTd.append(linkForEdit);

	const linkForDelete = document.createElement("a");
	linkForDelete.setAttribute("film-id", film.id);
	linkForDelete.append("Удалить");
	linkForDelete.addEventListener("click", event => {
		event.preventDefault();
		DeleteFilmById(film.id)
	});
	linksTd.append(linkForDelete);

	tr.append(linksTd);
	return tr;
}

