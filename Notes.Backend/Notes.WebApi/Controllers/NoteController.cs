using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Notes.WebApi.Controllers
{
    //[ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    [ApiVersionNeutral]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class NoteController:BaseController
    {
        private readonly IMapper _mapper;
        public NoteController(IMapper mapper)
        {
            _mapper = mapper;
        }


        /// <summary>
        /// Gets the list notes
        /// </summary>
        ///<remarks>
        ///Sample request:
        ///GET /note
        ///</remarks>
        /// <returns>Returns NoteListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized </response>
        [HttpGet]
       // [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NoteListVm>> GetAll() 
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the note by id
        /// </summary>
        ///<remarks>
        ///Sample request:
        ///GET /note/e0aab0b9-38e3-4c54-9813-6d53d2b77d8f
        ///</remarks>
        ///<param name="id">Note id (guid)</param>
        /// <returns>Returns NoteDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized </response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserId,
                Id=id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        /// <summary>
        /// Create the note
        /// </summary>
        ///<remarks>
        ///Sample request:
        ///POST /note/
        ///{
        ///     title:"note title",
        ///     details: "note details"
        ///}
        ///</remarks>
        ///<param name="createNoteDto">CreateNoteDto (object)</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized </response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NoteDetailsVm>> Create([FromBody]CreateNoteDto createNoteDto)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }


        /// <summary>
        /// Create the note
        /// </summary>
        ///<remarks>
        ///Sample request:
        ///PUT /note/
        ///{
        ///     title:"updated note title",
        ///}
        ///</remarks>
        ///<param name="updateNoteDto">UpdateNoteDto (object)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized </response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }


        /// <summary>
        /// Create the note
        /// </summary>
        ///<remarks>
        ///Sample request:
        ///DELETE /note/e0aab0b9-38e3-4c54-9813-6d53d2b77d8f
        ///</remarks>
        ///<param name="id">Id of the note (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized </response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                Id = id,
                UserId = UserId

            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
