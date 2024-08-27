﻿using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.SpecialOfferServices;

public class SpecialOfferService : ISpecialOfferService
{
    private readonly IMongoCollection<SpecialOffer> _specialOfferCollection;
    private readonly IMapper _mapper;

    public SpecialOfferService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString); //Connection
        var database = client.GetDatabase(_databaseSettings.DatabaseName); //Database
        _specialOfferCollection = database.GetCollection<SpecialOffer>(_databaseSettings.SpecialOfferCollectionName); //Table
        _mapper = mapper;
    }

    public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
    {
        var values = _mapper.Map<SpecialOffer>(createSpecialOfferDto);
        await _specialOfferCollection.InsertOneAsync(values);
    }

    public async Task DeleteSpecialOfferAsync(string id)
    {
        await _specialOfferCollection.DeleteOneAsync(x => x.SpecialOfferId == id);
    }

    public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
    {
        var values = await _specialOfferCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<ResultSpecialOfferDto>>(values);
    }

    public async Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
    {
        var values = await _specialOfferCollection.Find(x => x.SpecialOfferId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetByIdSpecialOfferDto>(values);
    }

    public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
    {
        var values = _mapper.Map<SpecialOffer>(updateSpecialOfferDto);
        await _specialOfferCollection.FindOneAndReplaceAsync(x => x.SpecialOfferId == updateSpecialOfferDto.SpecialOfferId, values);
    }
}
