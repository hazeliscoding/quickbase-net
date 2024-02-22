using System;
using System.Collections.Generic;
using QuickbaseNet.Models;
using QuickbaseNet.Requests;

namespace QuickbaseNet.Helpers
{
    /// <summary>
    /// Helper class for building QuickBase API commands.
    /// </summary>
    public class QuickbaseCommandBuilder
    {
        private string _tableId;
        private string _whereClauseForDeletion;
        private readonly List<Dictionary<string, FieldValue>> _records = new List<Dictionary<string, FieldValue>>();
        private int[] _fieldsToReturn;

        /// <summary>
        /// Specifies the table to operate on.
        /// </summary>
        /// <param name="tableId">The ID of the QuickBase table.</param>
        /// <returns>The current instance of QuickbaseCommandBuilder.</returns>
        public QuickbaseCommandBuilder ForTable(string tableId)
        {
            _tableId = tableId;
            return this;
        }

        /// <summary>
        /// Specifies which fields to return after the operation.
        /// </summary>
        /// <param name="fieldIds">The IDs of the fields to return.</param>
        /// <returns>The current instance of QuickbaseCommandBuilder.</returns>
        public QuickbaseCommandBuilder ReturnFields(params int[] fieldIds)
        {
            _fieldsToReturn = fieldIds;
            return this;
        }

        /// <summary>
        /// Adds a new record to the command being built.
        /// </summary>
        /// <param name="config">Action to configure the new record.</param>
        /// <returns>The current instance of QuickbaseCommandBuilder.</returns>
        public QuickbaseCommandBuilder AddNewRecord(Action<RecordBuilder> config)
        {
            var recordBuilder = new RecordBuilder();
            config(recordBuilder);
            _records.Add(recordBuilder.Build());
            return this;
        }

        /// <summary>
        /// Updates an existing record in the command being built.
        /// </summary>
        /// <param name="recordId">The ID of the record to update.</param>
        /// <param name="config">Action to configure the updated record.</param>
        /// <returns>The current instance of QuickbaseCommandBuilder.</returns>
        public QuickbaseCommandBuilder UpdateRecord(int recordId, Action<RecordBuilder> config)
        {
            var recordBuilder = new RecordBuilder();
            config(recordBuilder);

            // Assuming '3' is the default key field ID for record ID
            recordBuilder.AddField(3, recordId.ToString());
            _records.Add(recordBuilder.Build());
            return this;
        }

        /// <summary>
        /// Specifies deletion criteria for records.
        /// </summary>
        /// <param name="whereClause">The deletion criteria.</param>
        /// <returns>The current instance of QuickbaseCommandBuilder.</returns>
        public QuickbaseCommandBuilder WithDeletionCriteria(string whereClause)
        {
            _whereClauseForDeletion = whereClause;
            return this;
        }

        /// <summary>
        /// Builds an insert or update command.
        /// </summary>
        /// <returns>An InsertOrUpdateRecordRequest object representing the command.</returns>
        public InsertOrUpdateRecordRequest BuildInsertUpdateCommand()
        {
            return new InsertOrUpdateRecordRequest
            {
                To = _tableId,
                Data = _records,
                FieldsToReturn = _fieldsToReturn
            };
        }

        /// <summary>
        /// Builds a delete command.
        /// </summary>
        /// <returns>A DeleteRecordRequest object representing the command.</returns>
        public DeleteRecordRequest BuildDeleteCommand()
        {
            return new DeleteRecordRequest
            {
                From = _tableId,
                Where = _whereClauseForDeletion
            };
        }

        /// <summary>
        /// Builder class for constructing record objects.
        /// </summary>
        public class RecordBuilder
        {
            private readonly Dictionary<string, FieldValue> _fields = new Dictionary<string, FieldValue>();

            /// <summary>
            /// Adds a field to the record being built.
            /// </summary>
            /// <typeparam name="T">The type of the field value.</typeparam>
            /// <param name="fieldId">The ID of the field.</param>
            /// <param name="value">The value of the field.</param>
            /// <returns>The current instance of RecordBuilder.</returns>
            public RecordBuilder AddField<T>(int fieldId, T value)
            {
                _fields[fieldId.ToString()] = new FieldValue { Value = value };
                return this;
            }

            /// <summary>
            /// Adds multiple fields to the record being built.
            /// </summary>
            /// <param name="fields"></param>
            /// <returns></returns>
            public RecordBuilder AddFields(params (int fieldId, dynamic value)[] fields)
            {
                foreach (var field in fields)
                {
                    AddField(field.fieldId, field.value);
                }
                return this;
            }

            /// <summary>
            /// Builds the record.
            /// </summary>
            /// <returns>A dictionary representing the record.</returns>
            public Dictionary<string, FieldValue> Build()
            {
                return _fields;
            }
        }
    }
}
